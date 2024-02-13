using HITSBackEnd.DataBaseContext;
using HITSBackEnd.DataValidation;
using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Exceptions;
using HITSBackEnd.Models.AccountModels;
using HITSBackEnd.Repository.UserRepository;
using HITSBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Services.User.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly TokenGenerator _tokenGenerator;
        private readonly AddressValidator _addressValidator;
        private readonly RedisRepository _redisRepository;

        public UserService(AppDbContext db, TokenGenerator tokenGenerator, AddressValidator addressValidator, RedisRepository redisRepository)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
            _addressValidator = addressValidator;
            _redisRepository = redisRepository;
        }

        public bool IsUniqueUser(string email)
        {
            return _db.Users.Any(user => user.Email == email);
        }

        public async Task<RegistrationLoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == loginRequestDto.Email);

            if (user == null || !PasswordVerificator.VerifyPassword(user.Password, loginRequestDto.Password))
            {
                throw new BadRequestException("Неверный email или пароль");
            }
            RegistrationLoginResponseDTO response = new RegistrationLoginResponseDTO
            {
                Token = _tokenGenerator.GenerateToken(loginRequestDto.Email)
            };
            return response;
        }

        public async Task<RegistrationLoginResponseDTO> Register(RegistrationRequestDTO registrationRequestDto)
        {
            if (IsUniqueUser(registrationRequestDto.Email))
            {
                throw new BadRequestException("Пользователь с таким email уже существует");
            }

            if (!UserInfoValidator.ValidatePhoneNumber(registrationRequestDto.PhoneNumber))
            {
                throw new BadRequestException("Неверный формат телефона");
            }

            var passwordValidation = UserInfoValidator.ValidatePassword(registrationRequestDto.Password);
            if (passwordValidation != "")
            {
                throw new BadRequestException(passwordValidation);
            }

            if (!UserInfoValidator.ValidateBirthDate(registrationRequestDto.BirthDate))
            {
                throw new BadRequestException("Нереалистичная дата рождения");
            }
            /*if (!_addressValidator.isAddressExist(registrationRequestDTO.Address))
            {
                throw new NotFoundException("Такого адресса не существует");
            }*/
            if (!UserInfoValidator.ValidateEmail((registrationRequestDto.Email)))
            {
                throw new BadRequestException("Неправильный формат email адреса");
            }

            var newUser = new UsersTable
            {
                Id = Guid.NewGuid(),
                FullName = registrationRequestDto.FullName,
                Email = registrationRequestDto.Email,
                Password = HashPassword(registrationRequestDto.Password),
                Gender = registrationRequestDto.Gender,
                Address = registrationRequestDto.Address,
                BirthDate = registrationRequestDto.BirthDate,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            var response = new RegistrationLoginResponseDTO
            {
                Token = _tokenGenerator.GenerateToken(registrationRequestDto.Email)
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
            if (email == null)
            {
                throw new BadRequestException("Токен не валиден");
            }

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
            if (email == null)
            {
                throw new BadRequestException("Токен не валиден");
            }

            await _redisRepository.AddTokenBlackList(token);
        }

        public async Task EditUserInfo(EditUserInfoRequestDTO userUpdateData, string email)
        {

            if (email == null)
            {
                throw new NotFoundException("Такого пользователя не существует");
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                user.FullName = userUpdateData.FullName ?? user.FullName;
                user.Gender = userUpdateData.Gender ?? user.Gender;

                if (userUpdateData.AddressId != null)
                {
                    if (!_addressValidator.isAddressExist((Guid)userUpdateData.AddressId))
                    {
                        throw new NotFoundException("Адрес не существует");
                    }

                    user.Address = (Guid)userUpdateData.AddressId;
                }

                if (!UserInfoValidator.ValidateBirthDate(userUpdateData.BirthDate))
                {
                    throw new BadRequestException("Нереалистичная дата рождения");
                }

                user.BirthDate = userUpdateData.BirthDate;

                if (userUpdateData.PhoneNumber != null)
                {
                    if (!UserInfoValidator.ValidatePhoneNumber(userUpdateData.PhoneNumber))
                    {
                        throw new BadRequestException("Неверный формат номера телефона");
                    }

                    user.PhoneNumber = userUpdateData.PhoneNumber;
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
