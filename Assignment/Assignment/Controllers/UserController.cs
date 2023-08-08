using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserDbContext db;
        public UserController(IConfiguration config, UserDbContext db)
        {
            this.config = config;
            this.db = db;
        }

        private string GenerateJwtToken(UserModel user)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));
            var signin = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var jwtsecuritytoken = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: null,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(config["Jwt:Expiration"])),
                signingCredentials: signin
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
            return token;
        }
        private UserModel Authenticate(UserModel user)
        {
            var userdata = db.Users.FirstOrDefault(
                u =>
                    u.Username.Equals(user.Username) &&
                    u.Password.Equals(user.Password)
            );
            if (userdata != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        [HttpPost("registration")]
        public ActionResult Registration([FromBody] UserModel user)
        {
            try
            {
                var singleid = db.Users.Any(d => d.StudentId == user.StudentId);
                if (singleid)
                {
                    return BadRequest("Student id is exists please insert different student id");
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Ok("User register successful");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] UserModel user)
        {
            var userlogin = Authenticate(user);
            if (userlogin != null)
            {
                var token = GenerateJwtToken(user);
                if (token != null)
                {
                    return Ok(new { Token = token, Time = DateTime.Now.ToString("F") });
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid Token" });
                }
            }
            else
            {
                return BadRequest("Invalid User Please Register First");
            }

        }
    }
}
