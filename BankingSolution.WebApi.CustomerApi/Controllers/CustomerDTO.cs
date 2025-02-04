using System.ComponentModel.DataAnnotations;
using BankingSolution.Domain.Entities;

namespace BankingSolution.WebApi.CustomerApi.Controllers
{
    /// <summary>
    /// Represents a data transfer object for a customer.
    /// </summary>
    public class CustomerDTO
    {
        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the customer.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        [Required]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth of the customer.
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Converts the <see cref="CustomerDTO"/> to a <see cref="Customer"/> entity.
        /// </summary>
        /// <returns>A <see cref="Customer"/> entity.</returns>
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
