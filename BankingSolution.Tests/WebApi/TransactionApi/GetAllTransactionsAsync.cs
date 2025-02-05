using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.TransactionApi
{
    public class GetAllTransactionsAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public GetAllTransactionsAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ShouldReturnAllTransactions()
        {
            var transactions = _moqSetup.MockTransactionsList;

            var result = await _moqSetup.TransactionController.GetAllTransactionsAsync();

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var transactionsResult = okResult.Value.Should().BeOfType<List<Transaction>>().Subject;
            transactionsResult.Should().BeEquivalentTo(transactions);
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ShouldReturnOkResult()
        {
            var result = await _moqSetup.TransactionController.GetAllTransactionsAsync();

            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
