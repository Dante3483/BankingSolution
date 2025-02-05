using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;
using Moq;

namespace BankingSolution.Tests.Repositories.AccountRepository
{
    public class DeleteAccountAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public DeleteAccountAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldDeleteAccountFromDatabase()
        {
            var account = _moqSetup.MockAccountsList.First();

            await _moqSetup.AccountRepository.DeleteAccountAsync(account.Id);

            _moqSetup.MockDbContext.Verify(db => db.Accounts.Remove(account), Times.Once);
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldCallSaveChangesAsync()
        {
            var account = _moqSetup.MockAccountsList.First();

            await _moqSetup.AccountRepository.DeleteAccountAsync(account.Id);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldThrowException_WhenAccountNotFound()
        {
            var account = new Account();

            var result = async () =>
                await _moqSetup.AccountRepository.DeleteAccountAsync(account.Id);

            await result.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
