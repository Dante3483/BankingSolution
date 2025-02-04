using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Repositories
{
    /// <summary>
    /// Provides the implementation for transaction repository operations.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context to be used by the repository.</param>
        public TransactionRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a new transaction to the repository.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _dbContext.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a transaction by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the transaction is not found.</exception>
        public async Task DeleteTransactionAsync(Guid id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction is null)
            {
                throw new InvalidOperationException("Transaction not found");
            }
            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of transactions.</returns>
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        /// <summary>
        /// Gets a transaction by its id.
        /// </summary>
        /// <param name="transactionId">The unique identifier of the transaction.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the transaction if found; otherwise, null.</returns>
        public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
        {
            return await _dbContext.Transactions.FindAsync(id);
        }
    }
}
