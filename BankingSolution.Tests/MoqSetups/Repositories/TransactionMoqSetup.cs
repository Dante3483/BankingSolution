using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Infrastructure.Contexts;
using BankingSolution.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace BankingSolution.Tests.MoqSetups.Repositories
{
    public class TransactionMoqSetup
    {
        public readonly Mock<BankingDbContext> MockDbContext;
        public readonly List<Transaction> MockTransactionsList;
        public readonly TransactionRepository TransactionRepository;

        public TransactionMoqSetup()
        {
            MockTransactionsList = new List<Transaction>
            {
                new Transaction { Amount = 100, TransactionType = TransactionType.Deposit },
                new Transaction { Amount = 200, TransactionType = TransactionType.Withdrawal },
            };

            MockDbContext = new Mock<BankingDbContext>();
            SetupMockContext();
            TransactionRepository = new(MockDbContext.Object);
        }

        private void SetupMockContext()
        {
            MockDbContext.Setup(x => x.Transactions).ReturnsDbSet(MockTransactionsList);
            MockDbContext
                .Setup(x => x.Transactions.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(
                    (object[] ids) => MockTransactionsList.FirstOrDefault(a => a.Id == (Guid)ids[0])
                );
        }
    }
}
