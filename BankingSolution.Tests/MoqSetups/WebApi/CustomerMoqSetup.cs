using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using BankingSolution.WebApi.CustomerApi.Controllers;
using Moq;

namespace BankingSolution.Tests.MoqSetups.WebApi
{
    public class CustomerMoqSetup
    {
        public readonly Mock<ICustomerRepository> MockCustomerRepository;
        public readonly List<Customer> MockCustomersList;
        public readonly CustomerController CustomerController;

        public CustomerMoqSetup()
        {
            MockCustomersList = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe" },
                new Customer { FirstName = "Jane", LastName = "Doe" },
            };

            MockCustomerRepository = new();
            SetupCustomerRepository();
            CustomerController = new(MockCustomerRepository.Object);
        }

        private void SetupCustomerRepository()
        {
            MockCustomerRepository
                .Setup(x => x.GetAllCustomersAsync())
                .ReturnsAsync(MockCustomersList);
            MockCustomerRepository
                .Setup(x => x.GetCustomerByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockCustomersList.FirstOrDefault(c => c.Id == id));
            MockCustomerRepository
                .Setup(x => x.DeleteCustomerAsync(It.IsAny<Guid>()))
                .Callback(
                    (Guid id) =>
                    {
                        var customer = MockCustomersList.FirstOrDefault(c => c.Id == id);
                        if (customer is not null)
                        {
                            MockCustomersList.Remove(customer);
                        }
                    }
                );
            MockCustomerRepository
                .Setup(x => x.AddCustomerAsync(It.IsAny<Customer>()))
                .Callback(
                    (Customer customer) =>
                    {
                        MockCustomersList.Add(customer);
                    }
                );
        }
    }
}
