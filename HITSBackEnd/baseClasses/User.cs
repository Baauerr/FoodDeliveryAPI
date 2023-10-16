using Microsoft.AspNetCore.Identity;

namespace HITSBackEnd.baseClasses
{
    public class User: IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthData { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
