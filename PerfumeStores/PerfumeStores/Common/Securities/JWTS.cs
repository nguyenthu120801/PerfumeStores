using Microsoft.IdentityModel.Tokens;
using PerfumeStores.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common.Securities
{
    public class JWTS
    {
        private readonly IConfiguration _configuration;

        public JWTS(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Customer user, bool isAcces)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            string exp = (isAcces) ? "Jwt:ExpireAccessToken" : "Jwt:ExpireRefreshToken";
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.CustomerId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.Phone),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : ""),
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration[exp])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
