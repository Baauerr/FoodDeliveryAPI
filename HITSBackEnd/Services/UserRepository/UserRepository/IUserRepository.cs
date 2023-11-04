using HITSBackEnd.Dto.UserDTO;

namespace HITSBackEnd.Services.UserRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        public Task<RegistrationLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        public Task<RegistrationLoginResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        public Task<ProfileResponseDTO> Profile(string name);
        public Task LogOut(string name, string email);

        public Task EditUserInfo(EditUserInfoRequestDTO userUpdateData, string email);
    }
}
