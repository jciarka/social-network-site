using BD2.API.Configuration;
using BD2.API.Database;
using BD2.API.Database.Entities;
using BD2.API.Models;
using BD2.API.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ExtendedControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly AppDbContext _authDbContext;

        public AuthController(
            UserManager<Account> userManager,
            TokenConfiguration tokenConfiguration
            )
        {
            _userManager = userManager;
            _tokenConfiguration = tokenConfiguration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // 1. Check, if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseBase()
                {
                    Success = false,
                    Errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList()
                });
            }

            // 2. Check if user exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return BadRequest(new ResponseBase()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        $"Email {request.Email} jest już zajęty"
                    }
                });
            }

            // 3. Create user
            var newUser = new Account()
            {
                UserName = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email,
            };
            var isCreated = await _userManager.CreateAsync(newUser, request.Password);

            if (!isCreated.Succeeded)
            {
                return BadRequest(new ResponseBase()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Nie można utworzyć użytkownika, spróbuj ponownie później"
                    }
                });
            }
            return Ok(new ResponseBase { Success = true });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Check model state validation
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseBase()
                {
                    Success = false,
                    Errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList()
                });
            }

            // 2. Look for user - check if exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser == null)
            {
                return BadRequest(new ResponseBase()
                {
                    Success = false,
                    Errors = new List<string>() { "Błedny email lub hasło" }
                });
            }

            // 3. Check password
            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!isCorrect)
            {
                return BadRequest(new ResponseBase()
                {
                    Success = false,
                    Errors = new List<string>() { "Błedny email lub hasło" }
                });
            }

            // 4. Generate token
            var jwtToken = await GenerateToken(existingUser);
            var roles = await _userManager.GetRolesAsync(existingUser);

            return Ok(new LoginResponse()
            {
                Firstname = existingUser.Firstname,
                Lastname = existingUser.Lastname,
                Roles  = roles,
                Token = jwtToken,
                Success = true,
            });
        }

        private async Task<string> GenerateToken(Account existingUser)
        {
            // Get description key
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.SecurityKey);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim("Id", existingUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, existingUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
            });

            // add role claims
            foreach (var role in await _userManager.GetRolesAsync(existingUser))
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            // prepare Token content and configuration
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60 * 24), // one day
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512)
            };

            // generate token
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenObject = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(tokenObject);
        }
    }
}
