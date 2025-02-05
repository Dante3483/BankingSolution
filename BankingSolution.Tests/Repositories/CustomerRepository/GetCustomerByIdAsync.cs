using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;

namespace BankingSolution.Tests.Repositories.CustomerRepository
{
    public class GetCustomerByIdAsync
    {
        private readonly CustomerMoqSetup _moqSetup;

        public GetCustomerByIdAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
        {
            var customer = _moqSetup.MockCustomersList.First();

            var result = await _moqSetup.CustomerRepository.GetCustomerByIdAsync(customer.Id);

            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            var customer = new Customer();

            var result = await _moqSetup.CustomerRepository.GetCustomerByIdAsync(customer.Id);

            result.Should().BeNull();
        }
    }
}
