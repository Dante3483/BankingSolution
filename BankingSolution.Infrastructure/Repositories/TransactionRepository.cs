using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingDbContext _dbContext;

        public TransactionRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _dbContext.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction is not null)
            {
                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
        {
            return await _dbContext.Transactions.FindAsync(id);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            var transactionToUpdate = await _dbContext.Transactions.FindAsync(transaction.Id);
            if (transactionToUpdate is not null)
            {
                _dbContext.Transactions.Update(transaction);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
