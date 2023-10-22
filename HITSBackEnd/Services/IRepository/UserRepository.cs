using HITSBackEnd.baseClasses;
using HITSBackEnd.DataBase;
using HITSBackEnd.Dto;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace HITSBackEnd.Services.IRepository
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _db;
        private string secretKey;
        private TokenGenerator _tokenGenerator;

        public UserRepository(AppDbContext db, IConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings: Secret");
            _tokenGenerator = tokenGenerator;
        }

        public bool IsUniqueUser(string email)
        {
            return _db.Users.Any(user => user.Email == email);
        }

        public async Task<RegistrationLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower()==loginRequestDTO.Email.ToLower() &&
            u.Password == loginRequestDTO.Password);

            if (user == null)
            {
                throw new Exception(ErrorCreator.CreateError("Неверный email или пароль"));
            }
            RegistrationLoginResponseDTO response = new RegistrationLoginResponseDTO
            {
                Token = _tokenGenerator.GenerateToken(loginRequestDTO.Email)
            };
            return response;
        }

        public async Task<RegistrationLoginResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            if (IsUniqueUser(registrationRequestDTO.Email))
            {
               throw new Exception(ErrorCreator.CreateError("Пользователь с таким email уже существует"));
            }
            if (!DataValidator.ValidatePhoneNumber(registrationRequestDTO.PhoneNumber))
            {
                throw new Exception(ErrorCreator.CreateError("Неверный формат телефона"));
            }

            var newUser = new Users
            {
                FullName = registrationRequestDTO.FullName,
                Email = registrationRequestDTO.Email,
                Password = HashPassword(registrationRequestDTO.Password),
                Gender = registrationRequestDTO.Gender,
                Address = registrationRequestDTO.Address,
                BirthDate = registrationRequestDTO.BirthDate,
                PhoneNumber = registrationRequestDTO.PhoneNumber,
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            RegistrationLoginResponseDTO response = new RegistrationLoginResponseDTO
            {
                Token = _tokenGenerator.GenerateToken(registrationRequestDTO.Email)
            };
            return response;
        }
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

       
    }
}
