using HITSBackEnd.baseClasses;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

<<<<<<<< HEAD:HITSBackEnd/Services/UserRepository/UserRepository/UsersTable.cs
namespace HITSBackEnd.Services.Account.UserRepository
========
namespace HITSBackEnd.Services.UserRepository
>>>>>>>> editUserProfile:HITSBackEnd/Services/UserRepository/Users.cs
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
