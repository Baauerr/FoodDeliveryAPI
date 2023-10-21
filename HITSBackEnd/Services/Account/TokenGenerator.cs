using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HITSBackEnd.Services.Account
{
    public class TokenGenerator
    {
        public static string GenerateToken(string email, string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("vxUMx5pLcH1sTJuz6IoC4tbvheq7MsEN");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "HITSBackEnd",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Audience = "123"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
