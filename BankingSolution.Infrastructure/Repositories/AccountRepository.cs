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
            if (account is not null)
            {
                _dbContext.Accounts.Remove(account);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            var accountToUpdate = await _dbContext.Accounts.FindAsync(account.Id);
            if (accountToUpdate is not null)
            {
                _dbContext.Accounts.Update(account);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
