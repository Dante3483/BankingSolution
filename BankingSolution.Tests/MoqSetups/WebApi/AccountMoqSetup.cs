using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.WebApi.AccountApi.Controllers;
using Moq;

namespace BankingSolution.Tests.MoqSetups.WebApi
{
    public class AccountMoqSetup
    {
        public readonly Mock<IAccountRepository> MockAccountRepository;
        public readonly Mock<ICustomerRepository> MockCustomerRepository;
        public readonly List<Account> MockAccountsList;
        public readonly List<Customer> MockCustomersList;
        public readonly AccountController AccountController;

        public AccountMoqSetup()
        {
            MockCustomersList = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe" },
                new Customer { FirstName = "Jane", LastName = "Doe" },
            };
            MockAccountsList = new List<Account>
            {
                new Account { CustomerId = MockCustomersList[0].Id, CurrentBalance = 1000.00m },
                new Account { CustomerId = MockCustomersList[1].Id, CurrentBalance = 500.00m },
            };

            MockAccountRepository = new();
            MockCustomerRepository = new();
            SetupAccountRepository();
            SetupCustomerRepository();
            AccountController = new(MockAccountRepository.Object, MockCustomerRepository.Object);
        }

        private void SetupAccountRepository()
        {
            MockAccountRepository
                .Setup(x => x.GetAllAccountsAsync())
                .ReturnsAsync(MockAccountsList);
            MockAccountRepository
                .Setup(x => x.GetAccountByAccountNumberAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockAccountsList.FirstOrDefault(a => a.Id == id));
            MockAccountRepository
                .Setup(x => x.DeleteAccountAsync(It.IsAny<Guid>()))
                .Callback(
                    (Guid id) =>
                    {
                        var account = MockAccountsList.FirstOrDefault(a => a.Id == id);
                        if (account is not null)
                        {
                            MockAccountsList.Remove(account);
                        }
                    }
                );
            MockAccountRepository
                .Setup(x => x.AddAccountAsync(It.IsAny<Account>()))
                .Callback(
                    (Account account) =>
                    {
                        MockAccountsList.Add(account);
                    }
                );
        }

        private void SetupCustomerRepository()
        {
            MockCustomerRepository
                .Setup(x => x.GetCustomerByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockCustomersList.FirstOrDefault(c => c.Id == id));
        }
    }
}
