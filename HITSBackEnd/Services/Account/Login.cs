using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Provider;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HITSBackEnd.Services.Account
{
    public class Login
    {
        public string LogIn(string email, string password)
        {
            return "bruh";
        }
    }
}
