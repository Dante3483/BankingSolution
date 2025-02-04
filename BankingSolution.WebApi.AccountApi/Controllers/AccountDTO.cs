using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;

namespace BankingSolution.WebApi.AccountApi.Controllers
{
    public class AccountDTO
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        public DateTime? DateClosed { get; set; } = null;

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
