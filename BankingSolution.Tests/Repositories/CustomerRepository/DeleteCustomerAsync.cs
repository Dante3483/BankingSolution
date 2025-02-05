using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using FluentAssertions;
using Moq;

namespace BankingSolution.Tests.Repositories.CustomerRepository
{
    public class DeleteCustomerAsync
    {
        private readonly CustomerMoqSetup _moqSetup;

        public DeleteCustomerAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldDeleteCustomerFromDatabase()
        {
            var customer = _moqSetup.MockCustomersList.First();

            await _moqSetup.CustomerRepository.DeleteCustomerAsync(customer.Id);

            _moqSetup.MockDbContext.Verify(db => db.Customers.Remove(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldCallSaveChangesAsync()
        {
            var customer = _moqSetup.MockCustomersList.First();

            await _moqSetup.CustomerRepository.DeleteCustomerAsync(customer.Id);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldThrowException_WhenCustomerNotFound()
        {
            var customer = new Customer();

            var result = async () =>
                await _moqSetup.CustomerRepository.DeleteCustomerAsync(customer.Id);

            await result.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
