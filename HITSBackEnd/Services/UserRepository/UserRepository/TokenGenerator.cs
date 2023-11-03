using HITSBackEnd.DataBase;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HITSBackEnd.Services.UserRepository
{
    public class TokenGenerator
    {
        private string secretKey;

        public TokenGenerator(IConfiguration configuration)
        {
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }
        public string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, email)
                }),
<<<<<<<< HEAD:HITSBackEnd/Services/UserRepository/UserRepository/TokenGenerator.cs
                Issuer = "HITSBackend", 
                Audience = "HITSBackend"
========
                Issuer = "HITSBackend",
>>>>>>>> editUserProfile:HITSBackEnd/Services/UserRepository/TokenGenerator.cs
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
