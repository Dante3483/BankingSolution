using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingSolution.Domain.Enums;

namespace BankingSolution.Domain.Entities
{
    /// <summary>
    /// Represents a transaction entity.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the type of the transaction.
        /// </summary>
        [Required]
        [EnumDataType(typeof(TransactionType))]
        public TransactionType TransactionType { get; set; } = TransactionType.None;

        /// <summary>
        /// Gets or sets the amount of the transaction.
        /// </summary>
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date of the transaction.
        /// </summary>
        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the status of the transaction.
        /// </summary>
        [Required]
        [EnumDataType(typeof(TransactionStatus))]
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.None;

        /// <summary>
        /// Gets or sets the unique identifier for the account associated with the transaction.
        /// </summary>
        [Required]
        public Guid AccountId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the destination account, if applicable.
        /// </summary>
        public Guid? DestinationAccountId { get; set; }

        /// <summary>
        /// Gets or sets the account associated with the transaction.
        /// </summary>
        [ForeignKey(nameof(AccountId))]
        public virtual Account? Account { get; set; }

        /// <summary>
        /// Gets or sets the destination account associated with the transaction, if applicable.
        /// </summary>
        [ForeignKey(nameof(DestinationAccountId))]
        public virtual Account? DestinationAccount { get; set; }
    }
}
