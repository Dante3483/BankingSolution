using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.AccountApi
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

            var result = await _moqSetup.AccountController.GetAllAccountsAsync();

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var accountsResult = okResult.Value.Should().BeOfType<List<Account>>().Subject;
            accountsResult.Should().BeEquivalentTo(accounts);
        }

        [Fact]
        public async Task GetAllAccountsAsync_ShouldReturnOkResult()
        {
            var result = await _moqSetup.AccountController.GetAllAccountsAsync();

            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
