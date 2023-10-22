using HITSBackEnd.baseClasses;
using HITSBackEnd.Dto;
using HITSBackEnd.Services.Account;

namespace HITSBackEnd.Services.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        Task<RegistrationLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<RegistrationLoginResponseDTO> Register (RegistrationRequestDTO registrationRequestDTO);

    }
}
