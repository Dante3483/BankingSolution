using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Domain.Entities
{
    public class Customer
    {
        [Key]
        private Guid _id = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        private string _firstName = string.Empty;

        [Required]
        [MaxLength(50)]
        private string _lastName = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        private string _email = string.Empty;

        [Required]
        [MaxLength(100)]
        private string _address = string.Empty;

        [Required]
        [Phone]
        [MaxLength(15)]
        private string _phoneNumber = string.Empty;

        [Required]
        private DateTime _dateOfBirth;

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }
        public string Email
        {
            get => _email;
            set => _email = value;
        }
        public string Address
        {
            get => _address;
            set => _address = value;
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }
    }
}
