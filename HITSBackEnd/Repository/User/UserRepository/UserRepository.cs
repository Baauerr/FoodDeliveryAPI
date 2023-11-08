using HITSBackEnd.DataBaseContext;
using HITSBackEnd.DataValidation;
using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Models.AccountModels;
using HITSBackEnd.Models.DishesModels;
using HITSBackEnd.Repository.UserRepository;
using HITSBackEnd.Services.UserRepository;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Repository.User.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private TokenGenerator _tokenGenerator;
        private UserInfoValidator _userInfoValidator;
        private AddressValidator _addressValidator;

        public UserRepository(AppDbContext db, TokenGenerator tokenGenerator, UserInfoValidator userInfoValidator, AddressValidator addressValidator)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
            _userInfoValidator = userInfoValidator;
            _addressValidator = addressValidator;
        }

        public bool IsUniqueUser(string email)
        {
            return _db.Users.Any(user => user.Email == email);
        }

        public async Task<RegistrationLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == loginRequestDTO.Email);


            if (user == null || !PasswordVerificator.VerifyPassword(user.Password, loginRequestDTO.Password))
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

            if (!_userInfoValidator.ValidatePhoneNumber(registrationRequestDTO.PhoneNumber))
            {
                throw new BadRequestException("Неверный формат телефона");
            }

            var passwordValidation = _userInfoValidator.ValidatePassword(registrationRequestDTO.Password);
            if (passwordValidation != "")
            {
                throw new BadRequestException(passwordValidation);
            }

            if (!_userInfoValidator.ValidateBirthDate(registrationRequestDTO.BirthDate))
            {
                throw new BadRequestException("Нереалистичная дата рождения");
            }
            if (!_addressValidator.isAddressExist(registrationRequestDTO.Address))
            {
                throw new NotFoundException("Такого адресса не существует");
            }

            var newUser = new UsersTable
            {
                Id = Guid.NewGuid(),
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
        private string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword("2023", password);
        }

        public async Task<ProfileResponseDTO> Profile(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new NotFoundException("Такого пользователя не существует");   
            }

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
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            user.FullName = userUpdateData.FullName ?? user.FullName;
            user.BirthDate = userUpdateData.BirthDate ?? user.BirthDate;
            user.Gender = userUpdateData.Gender ?? user.Gender;
            user.Address = userUpdateData.AddressId ?? user.Address;

            if (userUpdateData.AddressId != null)
            {
                if (!_addressValidator.isAddressExist((Guid)userUpdateData.AddressId))
                {
                    throw new NotFoundException("Адрес не существует");
                }
                user.Address = (Guid)userUpdateData.AddressId;
            }

            if (userUpdateData.BirthDate != null)
            {
                if (!_userInfoValidator.ValidateBirthDate(userUpdateData.BirthDate.Value))
                {
                    throw new BadRequestException("Нереалистичная дата рождения");
                }
                user.BirthDate = userUpdateData.BirthDate.Value;
            }

            if (userUpdateData.PhoneNumber != null)
            {
                if (!_userInfoValidator.ValidatePhoneNumber(userUpdateData.PhoneNumber))
                {
                    throw new BadRequestException("Неверный формат номера телефона");
                }
                user.PhoneNumber = userUpdateData.PhoneNumber;
            }
            await _db.SaveChangesAsync();
        }
    }
}
