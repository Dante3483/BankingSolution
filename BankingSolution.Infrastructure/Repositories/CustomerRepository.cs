using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankingDbContext _dbContext;

        public CustomerRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Guid customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer is null)
            {
                throw new InvalidOperationException("Customer not found");
            }
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }
    }
}
