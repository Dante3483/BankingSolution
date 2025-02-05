using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using Moq;

namespace BankingSolution.Tests.Repositories.AccountRepository
{
    public class AddAccountAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public AddAccountAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddAccountAsync_ShouldAddAccountToDatabase()
        {
            var account = new Account();

            await _moqSetup.AccountRepository.AddAccountAsync(account);

            _moqSetup.MockDbContext.Verify(
                db => db.Accounts.AddAsync(account, default),
                Times.Once
            );
        }

        [Fact]
        public async Task AddAccountAsync_ShouldCallSaveChangesAsync()
        {
            var account = new Account();

            await _moqSetup.AccountRepository.AddAccountAsync(account);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}
