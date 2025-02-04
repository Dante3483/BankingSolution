using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _dbContext;

        public AccountRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await _dbContext.Accounts.FindAsync(id);
            if (account is null)
            {
                throw new InvalidOperationException("Account not found");
            }
            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Account?> GetAccountByAccountNumberAsync(Guid id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task IncreaseAccountBalanceAsync(Guid id, decimal amount)
        {
            var account = await GetAccountByAccountNumberAsync(id);
            if (account is null)
            {
                throw new InvalidOperationException("Account not found");
            }
            account.CurrentBalance += amount;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DecreaseAccountBalanceAsync(Guid id, decimal amount)
        {
            var account = await GetAccountByAccountNumberAsync(id);
            if (account is null)
            {
                throw new InvalidOperationException("Account not found");
            }
            if (account.CurrentBalance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            account.CurrentBalance -= amount;
            await _dbContext.SaveChangesAsync();
        }
    }
}
