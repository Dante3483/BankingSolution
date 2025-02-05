using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;
using Moq;

namespace BankingSolution.Tests.Repositories.AccountRepository
{
    public class IncreaseAccountBalanceAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public IncreaseAccountBalanceAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task IncreaseAccountBalanceAsync_ShouldIncreaseBalance_WhenAccountExists()
        {
            var account = _moqSetup.MockAccountsList.First();

            await _moqSetup.AccountRepository.IncreaseAccountBalanceAsync(account.Id, 100.00m);

            account.CurrentBalance.Should().Be(1100.00m);
        }

        [Fact]
        public async Task IncreaseAccountBalanceAsync_ShouldThrowException_WhenAccountDoesNotExist()
        {
            var account = new Account();

            var result = async () =>
                await _moqSetup.AccountRepository.IncreaseAccountBalanceAsync(account.Id, 100.00m);

            await result.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task IncreaseAccountBalanceAsync_ShouldCallSaveChangesAsync()
        {
            var account = _moqSetup.MockAccountsList.First();

            await _moqSetup.AccountRepository.IncreaseAccountBalanceAsync(account.Id, 100.00m);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}
