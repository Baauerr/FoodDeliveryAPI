using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.baseClasses
{
    public class Users
    {
        public string FullName { get; set; }
        public string Password { get; set; }    
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        [Key]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
