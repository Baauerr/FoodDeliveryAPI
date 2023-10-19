using HITSBackEnd.baseClasses;
using HITSBackEnd.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HITSBackEnd.Services
{
    public class Registration
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        Registration(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void RegistrateUser(String fullName, String password, String email, String adressId, DateTime birthDate, Gender Gender, String phoneNumber)
        {
            if (UserDublicateChecker(email))
            {
                throw new Exception("User with this email already exists");
            }
            if (!DataValidator.ValidatePhoneNumber(phoneNumber))
            {
                throw new Exception("The number does not match the format");
            }
            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                Password = HashPassword(password),
                Gender = Gender,
                Address = adressId,
                BirthData = birthDate,
                Phone = phoneNumber,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        
        private bool UserDublicateChecker(String email)
        {
            return (_context.Users.Any(user => user.Email == email));
        }
        private string HashPassword(String password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
    
}

