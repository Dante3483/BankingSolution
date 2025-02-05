using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Tests.MoqSetups.WebApi;
using BankingSolution.WebApi.TransactionApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.TransactionApi
{
    public class AddWithdrawTransactionAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public AddWithdrawTransactionAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddWithdrawTransactionAsync_ShouldReturnTransaction()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO() { AccountId = account.Id };

            var result = await _moqSetup.TransactionController.AddWithdrawTransactionAsync(
                transactionDTO
            );

            var createdAtRouteResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;

            var transactionResult = createdAtRouteResult
                .Value.Should()
                .BeOfType<Transaction>()
                .Subject;
            transactionResult.TransactionType.Should().Be(TransactionType.Withdrawal);
        }

        [Fact]
        public async Task AddWithdrawTransactionAsync_ShouldReturnCreatedAtRouteResult()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO() { AccountId = account.Id };

            var result = await _moqSetup.TransactionController.AddWithdrawTransactionAsync(
                transactionDTO
            );

            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task AddWithdrawTransactionAsync_ShouldReturnBadRequest_WhenTransactionDTOIsNull()
        {
            var result = await _moqSetup.TransactionController.AddWithdrawTransactionAsync(null);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddWithdrawTransactionAsync_ShouldReturnBadRequest_WhenInsufficientFunds()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO()
            {
                AccountId = account.Id,
                Amount = account.CurrentBalance * 2,
            };

            var result = await _moqSetup.TransactionController.AddWithdrawTransactionAsync(
                transactionDTO
            );

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddWithdrawTransactionAsync_ShouldDecreaseAccountBalance()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO() { AccountId = account.Id, Amount = 100m };
            var expectedBalance = account.CurrentBalance - transactionDTO.Amount;

            var result = await _moqSetup.TransactionController.AddWithdrawTransactionAsync(
                transactionDTO
            );

            account.CurrentBalance.Should().Be(expectedBalance);
        }
    }
}
