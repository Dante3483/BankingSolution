using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.Repositories;
using Moq;

namespace BankingSolution.Tests.Repositories.CustomerRepository
{
    public class AddCustomerAsync
    {
        private readonly CustomerMoqSetup _moqSetup;

        public AddCustomerAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldAddCustomerToDatabase()
        {
            var customer = new Customer();

            await _moqSetup.CustomerRepository.AddCustomerAsync(customer);

            _moqSetup.MockDbContext.Verify(
                db => db.Customers.AddAsync(customer, default),
                Times.Once
            );
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldCallSaveChangesAsync()
        {
            var customer = new Customer();

            await _moqSetup.CustomerRepository.AddCustomerAsync(customer);

            _moqSetup.MockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}
