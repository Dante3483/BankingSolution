using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;

namespace BankingSolution.WebApi.AccountApi.Controllers
{
    /// <summary>
    /// Represents a data transfer object for an account.
    /// </summary>
    public class AccountDTO
    {
        /// <summary>
        /// Gets or sets the id for the customer who owns the account.
        /// </summary>
        [Required]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the date the account was opened.
        /// </summary>
        [Required]
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date the account was closed.
        /// </summary>
        public DateTime? DateClosed { get; set; } = null;

        /// <summary>
        /// Converts the <see cref="AccountDTO"/> to an <see cref="Account"/> entity.
        /// </summary>
        /// <param name="initialBalance">The initial balance of the account.</param>
        /// <returns>An <see cref="Account"/> entity.</returns>
        public Account ToAccount(decimal initialBalance = 0m)
        {
            return new Account
            {
                CustomerId = CustomerId,
                CurrentBalance = initialBalance,
                DateOpened = DateOpened,
                DateClosed = DateClosed,
            };
        }
    }
}
