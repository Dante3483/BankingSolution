using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using Moq;

namespace BankingSolution.Tests.Repositories.TransactionRepository
{
    public class AddTransactionAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public AddTransactionAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddTransactionAsync_ShouldAddTransactionToDatabase()
        {
            var transaction = new Transaction();

            await _moqSetup.TransactionRepository.AddTransactionAsync(transaction);

            _moqSetup.MockDbContext.Verify(
                db => db.Transactions.AddAsync(transaction, default),
                Times.Once
            );
        }

        [Fact]
        public async Task AddTransactionAsync_ShouldCallSaveChangesAsync()
        {
            var transaction = new Transaction();

            await _moqSetup.TransactionRepository.AddTransactionAsync(transaction);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}
