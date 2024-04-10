using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementApi.Dto;
using StudentManagementApi.Mappings;
using StudentManagementApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] AccountRegisterDto userForRegistration)
        {
            var newUser = userForRegistration.MapAccountRegisterDtoToUser();
            var result = await _userManager.CreateAsync(newUser, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok("User created succesfully");
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                var token = CreateToken(user);
                return Ok(new
                {
                    Token = token,
                });
            }
            return Unauthorized();
        }

        ///methods for token

        private string CreateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwt = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt.GetSection("Key").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwt = _configuration.GetSection("Jwt");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwt.GetSection("Issuer").Value,
                audience: jwt.GetSection("Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials
                );
            return tokenOptions;
        }
    }
}