using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;

namespace BankingSolution.WebApi.TransactionApi.Controllers
{
    public class TransactionDTO
    {
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [EnumDataType(typeof(TransactionStatus))]
        public TransactionStatus TransactionStatus { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [DefaultValue(null)]
        public Guid? DestinationAccountId { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                Amount = Amount,
                TransactionDate = TransactionDate,
                TransactionStatus = TransactionStatus,
                AccountId = AccountId,
                DestinationAccountId = DestinationAccountId,
            };
        }
    }
}
