using HITSBackEnd.baseClasses;
using HITSBackEnd.Dto.UserDTO;
<<<<<<<< HEAD:HITSBackEnd/Services/UserRepository/UserRepository/IUserRepository.cs
using HITSBackEnd.Services.Account;
========
>>>>>>>> editUserProfile:HITSBackEnd/Services/UserRepository/IUserRepository.cs

namespace HITSBackEnd.Services.UserRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        Task<RegistrationLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<RegistrationLoginResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        public ProfileResponseDTO Profile(string name);

        public void LogOut(string name, string email);

        public void EditUserInfo(EditUserInfoRequestDTO userUpdateData, string email);
    }
}
