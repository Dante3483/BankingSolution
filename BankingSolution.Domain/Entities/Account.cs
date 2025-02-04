using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSolution.Domain.Entities
{
    /// <summary>
    /// Represents a banking account entity.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account (account number).
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the unique identifier for the customer who owns the account.
        /// </summary>
        [Required]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the current balance of the account.
        /// </summary>
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal CurrentBalance { get; set; } = 0;

        /// <summary>
        /// Gets or sets the date the account was opened.
        /// </summary>
        [Required]
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date the account was closed.
        /// </summary>
        public DateTime? DateClosed { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the account.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
    }
}
