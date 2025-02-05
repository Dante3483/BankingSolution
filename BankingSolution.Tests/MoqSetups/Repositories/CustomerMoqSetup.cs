using BankingSolution.Domain.Entities;
using BankingSolution.Infrastructure.Contexts;
using BankingSolution.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace BankingSolution.Tests.MoqSetups.Repositories
{
    public class CustomerMoqSetup
    {
        public readonly Mock<BankingDbContext> MockDbContext;
        public readonly List<Customer> MockCustomersList;
        public readonly CustomerRepository CustomerRepository;

        public CustomerMoqSetup()
        {
            MockCustomersList = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe" },
                new Customer { FirstName = "Jane", LastName = "Doe" },
            };

            MockDbContext = new Mock<BankingDbContext>();
            SetupMockContext();
            CustomerRepository = new(MockDbContext.Object);
        }

        private void SetupMockContext()
        {
            MockDbContext.Setup(x => x.Customers).ReturnsDbSet(MockCustomersList);
            MockDbContext
                .Setup(x => x.Customers.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(
                    (object[] ids) => MockCustomersList.FirstOrDefault(a => a.Id == (Guid)ids[0])
                );
        }
    }
}
