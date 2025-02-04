using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;

namespace BankingSolution.WebApi.TransactionApi.Controllers
{
    public class TransactionDTO
    {
        [Required]
        [EnumDataType(typeof(TransactionType))]
        public TransactionType TransactionType { get; set; } = TransactionType.None;

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Required]
        [EnumDataType(typeof(TransactionStatus))]
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.None;

        [Required]
        public Guid AccountId { get; set; }

        public Guid? DestinationAccountId { get; set; } = null;

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                TransactionType = TransactionType,
                Amount = Amount,
                TransactionDate = TransactionDate,
                TransactionStatus = TransactionStatus,
                AccountId = AccountId,
                DestinationAccountId = DestinationAccountId,
            };
        }
    }
}
