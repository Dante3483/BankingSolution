using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSolution.Domain.Entities
{
    public class Account
    {
        [Key]
        private Guid _id = Guid.NewGuid();

        [Required]
        private Guid _customerId;

        [Required]
        [MaxLength(50)]
        private string _accountNumber = string.Empty;

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        private decimal _currentBalance = 0;

        [Required]
        private DateTime _dateOpened = DateTime.UtcNow;

        private DateTime? _dateClosed;

        [ForeignKey(nameof(_customerId))]
        private Customer? _customer;

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }
        public Guid CustomerId
        {
            get => _customerId;
            set => _customerId = value;
        }
        public string AccountNumber
        {
            get => _accountNumber;
            set => _accountNumber = value;
        }
        public decimal CurrentBalance
        {
            get => _currentBalance;
            set => _currentBalance = value;
        }
        public DateTime DateOpened
        {
            get => _dateOpened;
            set => _dateOpened = value;
        }
        public DateTime? DateClosed
        {
            get => _dateClosed;
            set => _dateClosed = value;
        }
        public virtual Customer? Customer
        {
            get => _customer;
            set => _customer = value;
        }
    }
}
