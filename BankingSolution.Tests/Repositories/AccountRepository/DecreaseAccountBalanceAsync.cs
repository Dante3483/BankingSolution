using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;
using Moq;

namespace BankingSolution.Tests.Repositories.AccountRepository
{
    public class DecreaseAccountBalanceAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public DecreaseAccountBalanceAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task DecreaseAccountBalanceAsync_ShouldDecreaseBalance_WhenAccountExists()
        {
            var account = _moqSetup.MockAccountsList.First();

            await _moqSetup.AccountRepository.DecreaseAccountBalanceAsync(account.Id, 100.00m);

            account.CurrentBalance.Should().Be(900.00m);
        }

        [Fact]
        public async Task DecreaseAccountBalanceAsync_ShouldThrowException_WhenAccountDoesNotExist()
        {
            var account = new Account();

            var result = async () =>
                await _moqSetup.AccountRepository.DecreaseAccountBalanceAsync(account.Id, 100.00m);

            await result.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task DecreaseAccountBalanceAsync_ShouldCallSaveChangesAsync()
        {
            var account = _moqSetup.MockAccountsList.First();

            await _moqSetup.AccountRepository.DecreaseAccountBalanceAsync(account.Id, 100.00m);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}
