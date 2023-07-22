using Asp.net_Core_Web_API_Assignment_Backend.Models;
using Asp.net_Core_Web_API_Assignment_Backend.Repositories;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_Core_Web_API_Assignment_Backend.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserDbContext db;
        public UserController(UserDbContext db, IConfiguration config)
        {
            this.db = db;
            this.config = config;
        }
        [Authorize]
        // GET: api/<UserController>
        [HttpGet("all")]
        public ActionResult GetAll()
        {
            
            try
            {
                var data = new UserRepo(db).GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var data = new UserRepo(db).Get(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public ActionResult AddUser([FromBody] User user)
        {
            try
            {
                new UserRepo(db).Add(user);
                return Ok("User has been added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //POST api/<UserController>
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody]User user)
        {
            var userlogin = new UserRepo(db).Authenticate(user.Username, user.Password);
            if (userlogin != null)
            {
                var token = new TokenRepo(config).GenerateJwtToken(userlogin);
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
                return BadRequest("Invalid User Please Signed Up First");
            }
            
        }
        [HttpPut("update/{id}")]
        public ActionResult Update(int id, [FromBody] User user)
        {
            try
            {
                var existingUser = new UserRepo(db).Get(id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                else
                {
                    user.Id = id;
                    new UserRepo(db).Update(user);
                    return Ok(new { Message = "User updated successfully" });
                }
    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        // DELETE api/<UserController>/5
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = new UserRepo(db).Delete(id);
                if (data)
                {
                    return Ok(new { Success = "true", Message = "User delete successfully", Payload = data });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
       
        //[HttpPost("logout")]
        //[Authorize] 
        //public IActionResult Logout()
        //{
            // Get the current user's token
            //var token = HttpContext.Request.Headers["Authorization"].ToString();

            // Add the token to the list of revoked tokens
           

            //return Ok(new { message = "Logout successful",payload=token.Replace(token," ") });
        //}
    }
}
