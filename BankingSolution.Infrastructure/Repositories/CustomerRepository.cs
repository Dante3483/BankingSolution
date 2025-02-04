using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Repositories
{
    /// <summary>
    /// Provides the implementation for customer repository operations.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context to be used by the repository.</param>
        public CustomerRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a new customer to the repository.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a customer by their id.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the customer is not found.</exception>
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

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of customers.</returns>
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        /// <summary>
        /// Gets a customer by their id.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the customer if found; otherwise, null.</returns>
        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }
    }
}
