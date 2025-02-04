using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    /// <summary>
    /// Defines the repository interface for customer operations.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Gets a customer by their id.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the customer if found; otherwise, null.</returns>
        Task<Customer?> GetCustomerByIdAsync(Guid id);

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of customers.</returns>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddCustomerAsync(Customer customer);

        /// <summary>
        /// Deletes a customer by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCustomerAsync(Guid id);
    }
}
