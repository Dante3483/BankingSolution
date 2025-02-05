using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Tests.MoqSetups.WebApi;
using BankingSolution.WebApi.TransactionApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.TransactionApi
{
    public class AddTransferTransactionAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public AddTransferTransactionAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddTransferTransactionAsync_ShouldReturnTransaction()
        {
            var account = _moqSetup.MockAccountsList.First();
            var destinationAccount = _moqSetup.MockAccountsList.Skip(1).First();
            var transactionDTO = new TransactionDTO()
            {
                AccountId = account.Id,
                DestinationAccountId = destinationAccount.Id,
            };

            var result = await _moqSetup.TransactionController.AddTransferTransactionAsync(
                transactionDTO
            );

            var createdAtRouteResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var transactionResult = createdAtRouteResult
                .Value.Should()
                .BeOfType<Transaction>()
                .Subject;
            transactionResult.TransactionType.Should().Be(TransactionType.Transfer);
        }

        [Fact]
        public async Task AddTransferTransactionAsync_ShouldReturnCreatedAtRouteResult()
        {
            var account = _moqSetup.MockAccountsList.First();
            var destinationAccount = _moqSetup.MockAccountsList.Skip(1).First();
            var transactionDTO = new TransactionDTO()
            {
                AccountId = account.Id,
                DestinationAccountId = destinationAccount.Id,
            };

            var result = await _moqSetup.TransactionController.AddTransferTransactionAsync(
                transactionDTO
            );

            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task AddTransferTransactionAsync_ShouldReturnBadRequest_WhenTransactionDTOIsNull()
        {
            var result = await _moqSetup.TransactionController.AddTransferTransactionAsync(null);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddTransferTransactionAsync_ShouldReturnBadRequest_WhenDestinationAccountIdIsNull()
        {
            var account = _moqSetup.MockAccountsList.First();
            var transactionDTO = new TransactionDTO() { AccountId = account.Id };

            var result = await _moqSetup.TransactionController.AddTransferTransactionAsync(
                transactionDTO
            );

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddTransferTransactionAsync_ShouldReturnBadRequest_WhenInsufficientFunds()
        {
            var account = _moqSetup.MockAccountsList.First();
            var destinationAccount = _moqSetup.MockAccountsList.Skip(1).First();
            var transactionDTO = new TransactionDTO()
            {
                AccountId = account.Id,
                DestinationAccountId = destinationAccount.Id,
                Amount = account.CurrentBalance * 2,
            };

            var result = await _moqSetup.TransactionController.AddTransferTransactionAsync(
                transactionDTO
            );

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddTransferTransactionAsync_ShouldDecreaseSourceAccountBalance()
        {
            var account = _moqSetup.MockAccountsList.First();
            var destinationAccount = _moqSetup.MockAccountsList.Skip(1).First();
            var transactionDTO = new TransactionDTO()
            {
                AccountId = account.Id,
                DestinationAccountId = destinationAccount.Id,
                Amount = 100m,
            };
            var expectedBalance = account.CurrentBalance - transactionDTO.Amount;

            var result = await _moqSetup.TransactionController.AddTransferTransactionAsync(
                transactionDTO
            );

            account.CurrentBalance.Should().Be(expectedBalance);
        }

        [Fact]
        public async Task AddTransferTransactionAsync_ShouldIncreaseDestinationAccountBalance()
        {
            var account = _moqSetup.MockAccountsList.First();
            var destinationAccount = _moqSetup.MockAccountsList.Skip(1).First();
            var transactionDTO = new TransactionDTO()
            {
                AccountId = account.Id,
                DestinationAccountId = destinationAccount.Id,
                Amount = 100m,
            };
            var expectedBalance = destinationAccount.CurrentBalance + transactionDTO.Amount;

            var result = await _moqSetup.TransactionController.AddTransferTransactionAsync(
                transactionDTO
            );

            destinationAccount.CurrentBalance.Should().Be(expectedBalance);
        }
    }
}
