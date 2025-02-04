using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;

namespace BankingSolution.WebApi.TransactionApi.Controllers
{
    /// <summary>
    /// Represents a data transfer object for a transaction.
    /// </summary>
    public class TransactionDTO
    {
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
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the transaction.
        /// </summary>
        [Required]
        [EnumDataType(typeof(TransactionStatus))]
        public TransactionStatus TransactionStatus { get; set; }

        /// <summary>
        /// Gets or sets the id for the account associated with the transaction.
        /// </summary>
        [Required]
        public Guid AccountId { get; set; }

        /// <summary>
        /// Gets or sets the id for the destination account, if applicable.
        /// </summary>
        [DefaultValue(null)]
        public Guid? DestinationAccountId { get; set; }

        /// <summary>
        /// Converts the <see cref="TransactionDTO"/> to a <see cref="Transaction"/> entity.
        /// </summary>
        /// <param name="transactionType">The type of the transaction.</param>
        /// <returns>A <see cref="Transaction"/> entity.</returns>
        public Transaction ToTransaction(TransactionType transactionType = TransactionType.None)
        {
            return new Transaction
            {
                TransactionType = transactionType,
                Amount = Amount,
                TransactionDate = TransactionDate,
                TransactionStatus = TransactionStatus,
                AccountId = AccountId,
                DestinationAccountId = DestinationAccountId,
            };
        }
    }
}
