using AuthService.Interfaces;
using AuthService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Service
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthServiceImpl> _logger;
        public AuthServiceImpl(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<AuthServiceImpl> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<string> Login(User user)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            User? loggedUser = users.FirstOrDefault(u => u.Email == user.Email);
            if (loggedUser == null)
            {
                _logger.LogError($"[Login] [User: {user.Email}] - Attempted login with not registered email.");
                return null!;
            }
            if (!BCrypt.Net.BCrypt.Verify(user.Password, loggedUser.Password))
            {
                _logger.LogError($"[Login] [User: {user.Email}] - Attempted login with incorrect password.");
                return null!;
            }

            var token = GetToken(loggedUser);
            return token;
        }

        private string GetToken(User user)
        {
            var claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Email", user.Email),
                        new Claim("UserType", user.Type.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "defaultdefault11"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
