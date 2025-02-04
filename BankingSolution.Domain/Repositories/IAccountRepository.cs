using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByIdAsync(Guid id);
        Task<Account?> GetAccountByAccountNumberAsync(string accountNumber);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
    }
}
