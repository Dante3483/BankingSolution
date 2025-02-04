using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingSolution.Domain.Enums;

namespace BankingSolution.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public Guid? DestinationAccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account? Account { get; set; }

        [ForeignKey(nameof(DestinationAccountId))]
        public virtual Account? DestinationAccount { get; set; }
    }
}
