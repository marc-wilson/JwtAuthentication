using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.Controllers
{
    /// <summary>
    /// Main Authentication Controller. Based off https://github.com/medhatelmasry/JwtAuthentication/blob/master/JwtAuthentication/Controllers/AuthController.cs
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser()
        {
            var user = new ApplicationUser
            {
                UserName = "testuser@test.com"
            };
            var result = await _userManager.CreateAsync(user, "password123");
            if (!result.Succeeded) return new BadRequestObjectResult("Unable to create user");

            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn()
        {
            var user = await _userManager.FindByNameAsync("testuser@test.com");
            if (user == null) return new NotFoundObjectResult("User not found");
            var result = await _userManager.CheckPasswordAsync(user, "password123");
            if (!result) return new NotFoundObjectResult("Bad password");
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName)

            };
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(issuer: _configuration["JWT:Validator"], audience: _configuration["JWT:Validator"], expires: DateTime.UtcNow.AddMinutes(1), signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256));
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}