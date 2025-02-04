using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
    }
}
