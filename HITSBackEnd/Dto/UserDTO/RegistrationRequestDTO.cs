using HITSBackEnd.Models.AccountModels;
using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Dto.UserDTO
{
    public class RegistrationRequestDTO
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Неправильный формат адреса электронной почты.")]
        public string Email { get; set; }
        public Guid Address { get; set; }
    }
}
