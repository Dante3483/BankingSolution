using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    /// <summary>
    /// Defines the repository interface for account operations.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Gets an account by its account number (id).
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the account if found; otherwise, null.</returns>
        Task<Account?> GetAccountByAccountNumberAsync(Guid id);

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of accounts.</returns>
        Task<IEnumerable<Account>> GetAllAccountsAsync();

        /// <summary>
        /// Adds a new account.
        /// </summary>
        /// <param name="account">The account to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAccountAsync(Account account);

        /// <summary>
        /// Deletes an account by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the account to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAccountAsync(Guid id);

        /// <summary>
        /// Increases the balance of an account.
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <param name="amount">The amount to increase the balance.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task IncreaseAccountBalanceAsync(Guid id, decimal amount);

        /// <summary>
        /// Decreases the balance of an account.
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <param name="amount">The amount to decrease the balance.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DecreaseAccountBalanceAsync(Guid id, decimal amount);
    }
}
