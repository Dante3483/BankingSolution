using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;

namespace BankingSolution.Tests.Repositories.TransactionRepository
{
    public class GetTransactionByIdAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public GetTransactionByIdAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnTransaction_WhenTransactionExists()
        {
            var transaction = _moqSetup.MockTransactionsList.First();

            var result = await _moqSetup.TransactionRepository.GetTransactionByIdAsync(
                transaction.Id
            );

            result.Should().BeEquivalentTo(transaction);
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnNull_WhenTransactionDoesNotExist()
        {
            var transaction = new Transaction();

            var result = await _moqSetup.TransactionRepository.GetTransactionByIdAsync(
                transaction.Id
            );

            result.Should().BeNull();
        }
    }
}
