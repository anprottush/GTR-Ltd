using Asp.net_Core_Web_API_Assignment_Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Asp.net_Core_Web_API_Assignment_Backend.Repositories
{
    public class TokenRepo
    {
        private readonly IConfiguration config;
        public TokenRepo(IConfiguration config)
        {
            this.config = config;
        }
        public string GenerateJwtToken(User user)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));
            var signin = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[] {
                new Claim("UserId",user.Id.ToString()),
                new Claim("UserName",user.Username),
                new Claim("Password",user.Password)
            };
            var jwtsecuritytoken = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(config["Jwt:Expiration"])),
                signingCredentials: signin
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
            return token;
        }
    }
}
