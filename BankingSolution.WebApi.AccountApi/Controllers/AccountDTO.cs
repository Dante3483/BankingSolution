using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;

namespace BankingSolution.WebApi.AccountApi.Controllers
{
    public class AccountDTO
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        public DateTime? DateClosed { get; set; } = null;

        public Account ToAccount()
        {
            return new Account
            {
                CustomerId = CustomerId,
                AccountNumber = AccountNumber,
                DateOpened = DateOpened,
                DateClosed = DateClosed,
            };
        }
    }
}
