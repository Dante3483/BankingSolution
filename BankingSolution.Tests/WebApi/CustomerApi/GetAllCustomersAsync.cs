using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.CustomerApi
{
    public class GetAllCustomersAsync
    {
        private readonly CustomerMoqSetup _moqSetup;

        public GetAllCustomersAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            var customers = _moqSetup.MockCustomersList;

            var result = await _moqSetup.CustomerController.GetAllCustomersAsync();

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var customersResult = okResult.Value.Should().BeOfType<List<Customer>>().Subject;
            customersResult.Should().BeEquivalentTo(customers);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnOkResult()
        {
            var result = await _moqSetup.CustomerController.GetAllCustomersAsync();

            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
