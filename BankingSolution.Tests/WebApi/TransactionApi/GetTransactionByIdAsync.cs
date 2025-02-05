using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.TransactionApi
{
    public class GetTransactionByIdAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public GetTransactionByIdAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnTransactionWithCorrectProperties()
        {
            var transaction = _moqSetup.MockTransactionsList.First();

            var result = await _moqSetup.TransactionController.GetTransactionByIdAsync(
                transaction.Id
            );

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(transaction);
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnOkResult_WhenTransactionExists()
        {
            var transaction = _moqSetup.MockTransactionsList.First();

            var result = await _moqSetup.TransactionController.GetTransactionByIdAsync(
                transaction.Id
            );

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnNotFound_WhenTransactionDoesNotExist()
        {
            var transaction = new Transaction();

            var result = await _moqSetup.TransactionController.GetTransactionByIdAsync(
                transaction.Id
            );

            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
