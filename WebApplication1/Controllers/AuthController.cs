using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTO.Auth;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
    [FromBody] RefreshRequestDTO request)
        {
            var storedToken = await _context.RefreshToken
                .FirstOrDefaultAsync(x =>
                    x.Token == request.RefreshToken);

            if (storedToken == null ||
                storedToken.IsRevoked ||
                storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(
                storedToken.UserId);

            if (user == null)
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            storedToken.IsRevoked = true;

            var newRefreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(_configuration.GetValue<int>(
        "Jwt:RefreshTokenExpirationDays"
    )),
                IsRevoked = false
            };
            _context.RefreshToken.Add(newRefreshToken);
            var newAccessToken = CreateJWTToken(
       user,
       roles.ToList()
   );
            await _context.SaveChangesAsync();

            return Ok(new RefreshResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresIn = _configuration.GetValue<int>(
    "Jwt:AccessTokenExpirationMinutes"
) * 60
            });
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,

            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {

                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("user registered successfullyhi");
                    }
                }
            }
            return BadRequest("something went wrong");
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(
      [FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(
                loginRequestDTO.Username
            );

            if (user == null)
                return Unauthorized("Invalid email or password");

            var checkPasswordResult = await _userManager.CheckPasswordAsync(
                user,
                loginRequestDTO.Password
            );

            if (!checkPasswordResult)
                return Unauthorized("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = CreateJWTToken(
                user,
                roles.ToList()
            );

            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    _configuration.GetValue<int>(
                        "Jwt:RefreshTokenExpirationDays"
                    )
                ),
                IsRevoked = false
            };

            _context.RefreshToken.Add(refreshToken);

            await _context.SaveChangesAsync();

            var response = new RefreshResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = _configuration.GetValue<int>(
                    "Jwt:AccessTokenExpirationMinutes"
                ) * 60
            };

            return Ok(response);
        }

        private string CreateJWTToken(IdentityUser user, List<string> roles)
        {

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes")),
                claims: claims,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
