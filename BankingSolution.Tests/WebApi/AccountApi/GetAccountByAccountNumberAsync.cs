using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.AccountApi
{
    public class GetAccountByAccountNumberAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public GetAccountByAccountNumberAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetAccountByAccountNumberAsync_ShouldReturnAccountWithCorrectProperties()
        {
            var account = _moqSetup.MockAccountsList.First();
            var customer = _moqSetup.MockCustomersList.First(c => c.Id == account.CustomerId);

            var result = await _moqSetup.AccountController.GetAccountByAccountNumberAsync(
                account.Id
            );

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var accountResult = okResult.Value.Should().BeOfType<Account>().Subject;
            accountResult.Should().BeEquivalentTo(account);
            accountResult.Customer.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task GetAccountByAccountNumberAsync_ShouldReturnOkResult_WhenAccountExists()
        {
            var account = _moqSetup.MockAccountsList.First();

            var result = await _moqSetup.AccountController.GetAccountByAccountNumberAsync(
                account.Id
            );

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAccountByAccountNumberAsync_ShouldReturnNotFound_WhenAccountDoesNotExist()
        {
            var account = new Account();

            var result = await _moqSetup.AccountController.GetAccountByAccountNumberAsync(
                account.Id
            );

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAccountByAccountNumberAsync_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            var account = new Account();
            _moqSetup.MockAccountsList.Add(account);

            var result = await _moqSetup.AccountController.GetAccountByAccountNumberAsync(
                account.Id
            );

            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
