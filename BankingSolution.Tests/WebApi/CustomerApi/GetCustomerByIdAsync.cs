using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.CustomerApi
{
    public class GetCustomerByIdAsync
    {
        private readonly CustomerMoqSetup _moqSetup;

        public GetCustomerByIdAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomerWithCorrectProperties()
        {
            var customer = _moqSetup.MockCustomersList.First();

            var result = await _moqSetup.CustomerController.GetCustomerByIdAsync(customer.Id);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnOkResult_WhenCustomerExists()
        {
            var customer = _moqSetup.MockCustomersList.First();

            var result = await _moqSetup.CustomerController.GetCustomerByIdAsync(customer.Id);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            var customer = new Customer();

            var result = await _moqSetup.CustomerController.GetCustomerByIdAsync(customer.Id);

            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
