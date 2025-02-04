using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByIdAsync(Guid id);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
    }
}
