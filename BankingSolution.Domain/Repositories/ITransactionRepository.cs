using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    /// <summary>
    /// Defines the repository interface for transaction operations.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Gets a transaction by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the transaction if found; otherwise, null.</returns>
        Task<Transaction?> GetTransactionByIdAsync(Guid id);

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of transactions.</returns>
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

        /// <summary>
        /// Adds a new transaction.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddTransactionAsync(Transaction transaction);

        /// <summary>
        /// Deletes a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteTransactionAsync(Guid id);
    }
}
