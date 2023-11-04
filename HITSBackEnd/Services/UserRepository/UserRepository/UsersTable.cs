using HITSBackEnd.Services.Account.UserRepository;
using System.ComponentModel.DataAnnotations;
namespace HITSBackEnd.Services.UserRepository
{
    public class UsersTable
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }

        [Key]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Неправильный формат адреса электронной почты.")]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
