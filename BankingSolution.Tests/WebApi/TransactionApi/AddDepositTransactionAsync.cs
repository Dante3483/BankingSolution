using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Tests.MoqSetups.WebApi;
using BankingSolution.WebApi.TransactionApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.TransactionApi
{
    public class AddDepositTransactionAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public AddDepositTransactionAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddDepositTransactionAsync_ShouldReturnTransaction()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO() { AccountId = account.Id };

            var result = await _moqSetup.TransactionController.AddDepositTransactionAsync(
                transactionDTO
            );

            var createdAtRouteResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var transactionResult = createdAtRouteResult
                .Value.Should()
                .BeOfType<Transaction>()
                .Subject;
            transactionResult.TransactionType.Should().Be(TransactionType.Deposit);
        }

        [Fact]
        public async Task AddDepositTransactionAsync_ShouldReturnCreatedAtRouteResult()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO() { AccountId = account.Id };

            var result = await _moqSetup.TransactionController.AddDepositTransactionAsync(
                transactionDTO
            );

            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task AddDepositTransactionAsync_ShouldReturnBadRequest_WhenTransactionDTOIsNull()
        {
            var result = await _moqSetup.TransactionController.AddDepositTransactionAsync(null);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddDepositTransactionAsync_ShouldIncreaseAccountBalance()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO() { AccountId = account.Id, Amount = 100m };
            var expectedBalance = account.CurrentBalance + transactionDTO.Amount;

            var result = await _moqSetup.TransactionController.AddDepositTransactionAsync(
                transactionDTO
            );

            account.CurrentBalance.Should().Be(expectedBalance);
        }
    }
}
