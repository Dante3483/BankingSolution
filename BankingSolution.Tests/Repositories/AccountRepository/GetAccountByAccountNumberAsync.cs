using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;

namespace BankingSolution.Tests.Repositories.AccountRepository
{
    public class GetAccountByAccountNumberAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public GetAccountByAccountNumberAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetAccountByAccountNumberAsync_ShouldReturnAccount_WhenAccountExists()
        {
            var account = _moqSetup.MockAccountsList.First();

            var result = await _moqSetup.AccountRepository.GetAccountByAccountNumberAsync(
                account.Id
            );

            result.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task GetAccountByAccountNumberAsync_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            var account = new Account();

            var result = await _moqSetup.AccountRepository.GetAccountByAccountNumberAsync(
                account.Id
            );

            result.Should().BeNull();
        }
    }
}
