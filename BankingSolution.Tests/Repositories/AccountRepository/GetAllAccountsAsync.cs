using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;

namespace BankingSolution.Tests.Repositories.AccountRepository
{
    public class GetAllAccountsAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public GetAllAccountsAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetAllAccountsAsync_ShouldReturnAllAccounts()
        {
            var accounts = _moqSetup.MockAccountsList;

            var result = await _moqSetup.AccountRepository.GetAllAccountsAsync();

            result.Should().BeEquivalentTo(accounts);
        }
    }
}
