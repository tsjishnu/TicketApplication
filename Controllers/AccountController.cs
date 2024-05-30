using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using TicketApplication.Data;
using TicketApplication.Helpers;
using TicketApplication.Models;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TicketApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly FootballDbContext _context;

        public AccountController(FootballDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _context.UserTable.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User Not Found"});
            }
            if(!PasswordHasher.VerifyPassword(model.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is incorrect" });
            }
            user.Token = CreateJwt(user);

            return Ok(new { 
                Token = user.Token,
                Message = "Login Sucess"
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check Email
            var existingUser = await _context.UserTable.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User with this email already exists" });
            }
            //Check Password strength
            var pass = CheckPasswordStrength(model.Password);
            if(!string.IsNullOrEmpty(pass))
            {
                return BadRequest(new { message = pass.ToString() });
            }

            model.Password = PasswordHasher.HashPassword(model.Password);
            var newUser = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                Role = "User",
                Token = " "
            };
            _context.UserTable.Add(newUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Login), new { id = newUser.UserID }, newUser);
        }
        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if(password.Length < 8)
            {
                sb.Append("Minimum password length should be 8, "+Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be alphanumeric, " + Environment.NewLine);
            if (!(Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]")))
                sb.Append("Password should contain special characters, "+Environment.NewLine);
            return sb.ToString();
        }
        private string CreateJwt(User user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString())
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = credentials
            };
            var token = JwtTokenHandler.CreateToken(tokenDescriptor);
            return JwtTokenHandler.WriteToken(token);
        }


    }
}
