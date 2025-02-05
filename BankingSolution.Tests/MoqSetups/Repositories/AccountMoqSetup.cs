using BankingSolution.Domain.Entities;
using BankingSolution.Infrastructure.Contexts;
using BankingSolution.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace BankingSolution.Tests.MoqSetups.Repositories
{
    public class AccountMoqSetup
    {
        public readonly Mock<BankingDbContext> MockDbContext;
        public readonly List<Account> MockAccountsList;
        public readonly AccountRepository AccountRepository;

        public AccountMoqSetup()
        {
            MockAccountsList = new List<Account>
            {
                new Account { CurrentBalance = 1000.00m },
                new Account { CurrentBalance = 500.00m },
            };

            MockDbContext = new Mock<BankingDbContext>();
            SetupMockContext();
            AccountRepository = new(MockDbContext.Object);
        }

        private void SetupMockContext()
        {
            MockDbContext.Setup(x => x.Accounts).ReturnsDbSet(MockAccountsList);
            MockDbContext
                .Setup(x => x.Accounts.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(
                    (object[] ids) => MockAccountsList.FirstOrDefault(a => a.Id == (Guid)ids[0])
                );
        }
    }
}
