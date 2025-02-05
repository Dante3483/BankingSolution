using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using BankingSolution.WebApi.AccountApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.AccountApi
{
    public class AddAccountAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public AddAccountAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddAccountAsync_ShouldReturnAccount()
        {
            var initialBalance = 1000.0m;
            var accountDTO = new AccountDTO();

            var result = await _moqSetup.AccountController.AddAccountAsync(
                initialBalance,
                accountDTO
            );

            var createdAtRouteResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            createdAtRouteResult.Value.Should().BeOfType<Account>();
        }

        [Fact]
        public async Task AddAccountAsync_ShouldReturnCreatedAtRouteResult()
        {
            var initialBalance = 1000.0m;
            var accountDTO = new AccountDTO();

            var result = await _moqSetup.AccountController.AddAccountAsync(
                initialBalance,
                accountDTO
            );

            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task AddAccountAsync_ShouldReturnBadRequest_WhenInitialBalanceIsNegative()
        {
            var initialBalance = -1000.0m;
            var accountDTO = new AccountDTO();

            var result = await _moqSetup.AccountController.AddAccountAsync(
                initialBalance,
                accountDTO
            );

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddAccountAsync_ShouldReturnBadRequest_WhenAccountDTOIsNull()
        {
            var initialBalance = 1000.0m;

            var result = await _moqSetup.AccountController.AddAccountAsync(initialBalance, null);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
