using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Domain.Repositories;
using BankingSolution.WebApi.TransactionApi.Controllers;
using Moq;

namespace BankingSolution.Tests.MoqSetups.WebApi
{
    public class TransactionMoqSetup
    {
        public readonly Mock<ITransactionRepository> MockTransactionRepository;
        public readonly Mock<IAccountRepository> MockAccountRepository;
        public readonly List<Transaction> MockTransactionsList;
        public readonly List<Account> MockAccountsList;
        public readonly TransactionController TransactionController;

        public TransactionMoqSetup()
        {
            MockAccountsList = new List<Account>
            {
                new Account { CurrentBalance = 1000.00m },
                new Account { CurrentBalance = 500.00m },
            };
            MockTransactionsList = new List<Transaction>
            {
                new Transaction
                {
                    AccountId = MockAccountsList[0].Id,
                    Amount = 100.00m,
                    TransactionType = TransactionType.Deposit,
                },
                new Transaction
                {
                    AccountId = MockAccountsList[1].Id,
                    Amount = 100.00m,
                    TransactionType = TransactionType.Withdrawal,
                },
                new Transaction
                {
                    AccountId = MockAccountsList[0].Id,
                    DestinationAccountId = MockAccountsList[1].Id,
                    Amount = 50.00m,
                    TransactionType = TransactionType.Transfer,
                },
            };

            MockTransactionRepository = new();
            MockAccountRepository = new();
            SetupTransactionRepository();
            SetupAccountRepository();
            TransactionController = new(
                MockTransactionRepository.Object,
                MockAccountRepository.Object
            );
        }

        private void SetupTransactionRepository()
        {
            MockTransactionRepository
                .Setup(x => x.GetAllTransactionsAsync())
                .ReturnsAsync(MockTransactionsList);
            MockTransactionRepository
                .Setup(x => x.GetTransactionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockTransactionsList.FirstOrDefault(t => t.Id == id));
            MockTransactionRepository
                .Setup(x => x.DeleteTransactionAsync(It.IsAny<Guid>()))
                .Callback(
                    (Guid id) =>
                    {
                        var transaction = MockTransactionsList.FirstOrDefault(t => t.Id == id);
                        if (transaction is not null)
                        {
                            MockTransactionsList.Remove(transaction);
                        }
                    }
                );
            MockTransactionRepository
                .Setup(x => x.AddTransactionAsync(It.IsAny<Transaction>()))
                .Callback(
                    (Transaction transaction) =>
                    {
                        MockTransactionsList.Add(transaction);
                    }
                );
        }

        private void SetupAccountRepository()
        {
            MockAccountRepository
                .Setup(x => x.IncreaseAccountBalanceAsync(It.IsAny<Guid>(), It.IsAny<decimal>()))
                .Callback(
                    (Guid accountId, decimal amount) =>
                    {
                        var account = MockAccountsList.FirstOrDefault(a => a.Id == accountId);
                        if (account is not null)
                        {
                            account.CurrentBalance += amount;
                        }
                    }
                )
                .Returns(Task.CompletedTask);

            MockAccountRepository
                .Setup(x => x.DecreaseAccountBalanceAsync(It.IsAny<Guid>(), It.IsAny<decimal>()))
                .Callback(
                    (Guid accountId, decimal amount) =>
                    {
                        var account = MockAccountsList.FirstOrDefault(a => a.Id == accountId);
                        if (account is not null)
                        {
                            if (account.CurrentBalance < amount)
                            {
                                throw new InvalidOperationException("Insufficient funds.");
                            }
                            account.CurrentBalance -= amount;
                        }
                    }
                )
                .Returns(Task.CompletedTask);
        }
    }
}
