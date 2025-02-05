using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.AccountApi
{
    public class DeleteAccountAsync
    {
        private readonly AccountMoqSetup _moqSetup;

        public DeleteAccountAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldReturnNoContent_WhenAccountIsDeleted()
        {
            var account = _moqSetup.MockAccountsList.First();

            var result = await _moqSetup.AccountController.DeleteAccountAsync(account.Id);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldReturnNotFound_WhenAccountDoesNotExist()
        {
            var account = new Account();

            var result = await _moqSetup.AccountController.DeleteAccountAsync(account.Id);

            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
