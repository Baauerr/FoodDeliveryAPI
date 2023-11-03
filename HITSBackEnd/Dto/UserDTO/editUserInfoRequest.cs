using HITSBackEnd.baseClasses;

namespace HITSBackEnd.Dto.UserDTO
{
    public class EditUserInfoRequestDTO
    {
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? AddressId { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
