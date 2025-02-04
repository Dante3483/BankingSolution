using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Repositories
{
    /// <summary>
    /// Provides the implementation for account repository operations.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context to be used by the repository.</param>
        public AccountRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        /// <param name="account">The account to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an account by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the account to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the account is not found.</exception>
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

        /// <summary>
        /// Gets an account by its account number (id).
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the account if found; otherwise, null.</returns>
        public async Task<Account?> GetAccountByAccountNumberAsync(Guid id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of accounts.</returns>
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        /// <summary>
        /// Increases the balance of an account.
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <param name="amount">The amount to increase the balance.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the account is not found.</exception>
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

        /// <summary>
        /// Decreases the balance of an account.
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <param name="amount">The amount to decrease the balance by.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the account is not found or when insufficient funds.</exception>
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
