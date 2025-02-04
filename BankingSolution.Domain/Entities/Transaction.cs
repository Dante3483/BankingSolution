using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingSolution.Domain.Enums;

namespace BankingSolution.Domain.Entities
{
    public class Transaction
    {
        [Key]
        private Guid _id;

        [Required]
        [EnumDataType(typeof(TransactionType))]
        private TransactionType _transactionType = TransactionType.None;

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        private decimal _amount;

        [Required]
        private DateTime _transactionDate = DateTime.UtcNow;

        [Required]
        [EnumDataType(typeof(TransactionStatus))]
        private TransactionStatus _transactionStatus = TransactionStatus.None;

        [Required]
        private Guid _accountId;

        private Guid? _destinationAccountId;

        [ForeignKey(nameof(_accountId))]
        private Account? _account;

        [ForeignKey(nameof(_destinationAccountId))]
        private Account? _destinationAccount;

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }
        public TransactionType TransactionType
        {
            get => _transactionType;
            set => _transactionType = value;
        }
        public decimal Amount
        {
            get => _amount;
            set => _amount = value;
        }
        public DateTime TransactionDate
        {
            get => _transactionDate;
            set => _transactionDate = value;
        }
        public TransactionStatus TransactionStatus
        {
            get => _transactionStatus;
            set => _transactionStatus = value;
        }
        public Guid AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }
        public Guid? DestinationAccountId
        {
            get => _destinationAccountId;
            set => _destinationAccountId = value;
        }
        public virtual Account? Account
        {
            get => _account;
            set => _account = value;
        }
        public virtual Account? DestinationAccount
        {
            get => _destinationAccount;
            set => _destinationAccount = value;
        }
    }
}
