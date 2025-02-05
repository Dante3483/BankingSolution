using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;

namespace BankingSolution.Tests.Repositories.TransactionRepository
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

            var result = await _moqSetup.TransactionRepository.GetAllTransactionsAsync();

            result.Should().BeEquivalentTo(transactions);
        }
    }
}
