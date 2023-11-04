using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Services.Account;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Services.UserRepository
{
    public class UserRepository : IUserRepository
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
            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == loginRequestDTO.Email);


            if (user == null || !PasswordValidatoring.VerifyPassword(user.Password, loginRequestDTO.Password))
            {
                throw new BadRequestException("Неверный email или пароль");
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
                throw new BadRequestException("Пользователь с таким email уже существует");
            }
            if (!DataValidator.ValidatePhoneNumber(registrationRequestDTO.PhoneNumber))
            {
                throw new BadRequestException("Неверный формат телефона");
            }

            var newUser = new UsersTable
            {
                FullName = registrationRequestDTO.FullName,
                Email = registrationRequestDTO.Email,
                Password = HashPassword(registrationRequestDTO.Password),
                Gender = registrationRequestDTO.Gender,
                Address = registrationRequestDTO.Address,
                BirthDate = registrationRequestDTO.BirthDate,
                PhoneNumber = registrationRequestDTO.PhoneNumber,
            };

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            RegistrationLoginResponseDTO response = new RegistrationLoginResponseDTO
            {
                Token = _tokenGenerator.GenerateToken(registrationRequestDTO.Email)
            };
            return response;
        }
        public string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword("2023", password);
        }

        public async Task<ProfileResponseDTO> Profile(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            ProfileResponseDTO response = new ProfileResponseDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
            };
            return response;
        }

        public async Task LogOut(string token, string email)
        {
            BlackListTokenTable newToken = new BlackListTokenTable();
            newToken.Token = token;
            newToken.userEmail = email;
            await _db.BlackListTokens.AddAsync(newToken);
            await _db.SaveChangesAsync();
        }

        public async Task EditUserInfo(EditUserInfoRequestDTO userUpdateData, string email)
        {
            var user =  await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            user.FullName = userUpdateData.FullName ?? user.FullName;
            user.BirthDate = userUpdateData.BirthDate ?? user.BirthDate;
            user.Gender = userUpdateData.Gender ?? user.Gender;
            user.Address = userUpdateData.AddressId ?? user.Address;
            if (userUpdateData.PhoneNumber != null)
            {
                if (DataValidator.ValidatePhoneNumber(userUpdateData.PhoneNumber))
                {
                    user.PhoneNumber = userUpdateData.PhoneNumber;
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
