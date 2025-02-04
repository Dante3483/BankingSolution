using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByAccountNumberAsync(Guid id);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
        Task IncreaseAccountBalanceAsync(Guid id, decimal amount);
        Task DecreaseAccountBalanceAsync(Guid id, decimal amount);
    }
}
