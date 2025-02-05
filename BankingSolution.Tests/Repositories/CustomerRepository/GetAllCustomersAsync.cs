using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;

namespace BankingSolution.Tests.Repositories.CustomerRepository
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

            var result = await _moqSetup.CustomerRepository.GetAllCustomersAsync();

            result.Should().BeEquivalentTo(customers);
        }
    }
}
