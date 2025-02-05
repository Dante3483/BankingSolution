using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.CustomerApi
{
    public class DeleteCustomerAsync
    {
        private readonly CustomerMoqSetup _moqSetup;

        public DeleteCustomerAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldReturnNoContent_WhenCustomerIsDeleted()
        {
            var customer = _moqSetup.MockCustomersList.First();

            var result = await _moqSetup.CustomerController.DeleteCustomerAsync(customer.Id);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            var customer = new Customer();

            var result = await _moqSetup.CustomerController.DeleteCustomerAsync(customer.Id);

            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
