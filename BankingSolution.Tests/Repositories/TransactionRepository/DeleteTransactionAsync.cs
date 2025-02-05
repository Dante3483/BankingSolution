using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;
using Moq;

namespace BankingSolution.Tests.Repositories.TransactionRepository
{
    public class DeleteTransactionAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public DeleteTransactionAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task DeleteTransactionAsync_ShouldDeleteTransactionFromDatabase()
        {
            var transaction = _moqSetup.MockTransactionsList.First();

            await _moqSetup.TransactionRepository.DeleteTransactionAsync(transaction.Id);

            _moqSetup.MockDbContext.Verify(db => db.Transactions.Remove(transaction), Times.Once);
        }

        [Fact]
        public async Task DeleteTransactionAsync_ShouldCallSaveChangesAsync()
        {
            var transaction = _moqSetup.MockTransactionsList.First();

            await _moqSetup.TransactionRepository.DeleteTransactionAsync(transaction.Id);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteTransactionAsync_ShouldThrowException_WhenTransactionNotFound()
        {
            var transaction = new Transaction();

            var result = async () =>
                await _moqSetup.TransactionRepository.DeleteTransactionAsync(transaction.Id);

            await result.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
