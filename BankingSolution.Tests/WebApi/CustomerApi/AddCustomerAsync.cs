using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using BankingSolution.WebApi.CustomerApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.CustomerApi
{
    public class AddCustomerAsync
    {
        private readonly CustomerMoqSetup _moqSetup;

        public AddCustomerAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldReturnCustomer()
        {
            var customerDTO = new CustomerDTO();

            var result = await _moqSetup.CustomerController.AddCustomerAsync(customerDTO);

            var createdAtRouteResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            createdAtRouteResult.Value.Should().BeOfType<Customer>();
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldReturnCreatedAtRouteResult()
        {
            var customerDTO = new CustomerDTO();

            var result = await _moqSetup.CustomerController.AddCustomerAsync(customerDTO);

            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldReturnBadRequest_WhenCustomerDTOIsNull()
        {
            var result = await _moqSetup.CustomerController.AddCustomerAsync(null);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
