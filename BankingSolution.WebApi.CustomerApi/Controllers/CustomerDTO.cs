using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;

namespace BankingSolution.WebApi.CustomerApi.Controllers
{
    public class CustomerDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public Customer ToCustomer()
        {
            return new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Address = Address,
                PhoneNumber = PhoneNumber,
                DateOfBirth = DateOfBirth,
            };
        }
    }
}
