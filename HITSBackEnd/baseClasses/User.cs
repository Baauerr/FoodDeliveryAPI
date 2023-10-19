using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.baseClasses
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }    
        public DateTime BirthData { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
