﻿using HITSBackEnd.baseClasses;
using HITSBackEnd.Dto;
using HITSBackEnd.Services.Account;

namespace HITSBackEnd.Services.Account.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        Task<RegistrationLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<RegistrationLoginResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        public ProfileResponseDTO Profile( string name);

        public void LogOut(string name, string email);

        public void EditUserInfo(EditUserInfoRequestDTO userUpdateData, string email);
    }
}
