using BankingSolution.Domain.Entities;

namespace BankingSolution.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetTransactionByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(Guid id);
    }
}
