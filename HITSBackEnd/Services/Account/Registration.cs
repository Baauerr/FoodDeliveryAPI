using HITSBackEnd.baseClasses;
using HITSBackEnd.DataBase;
using System.Text;

namespace HITSBackEnd.Services.Account
{
    public class Registration
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public Registration(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void RegistrateUser(string fullName, string password, string email, string adressId, DateTime birthDate, Gender Gender, string phoneNumber)
        {
            if (UserDublicateChecker(email))
            {
                throw new Exception("User with this email already exists");
            }
            if (!DataValidator.ValidatePhoneNumber(phoneNumber))
            {
                throw new Exception("The number does not match the format");
            }
            var newUser = new Users
            {
                FullName = fullName,
                Email = email,
                Password = HashPassword(password),
                Gender = Gender,
                Address = adressId,
                BirthDate = birthDate,
                PhoneNumber = phoneNumber,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private bool UserDublicateChecker(string email)
        {
            return _context.Users.Any(user => user.Email == email);
        }
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}

