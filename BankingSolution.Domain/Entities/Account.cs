using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSolution.Domain.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal CurrentBalance { get; set; } = 0;

        [Required]
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        public DateTime? DateClosed { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
    }
}
