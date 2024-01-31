using AuthService.DTO;
using AuthService.Interfaces;
using AuthService.Models;
using AuthService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthServiceImpl> _logger;
        public AuthController(IAuthService authService, IMapper mapper, ILogger<AuthServiceImpl> logger)
        {
            _authService = authService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            User user = _mapper.Map<User>(userLoginDTO);
            string token = await _authService.Login(user);
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError($"[Login] [User: {userLoginDTO.Email}] credentials not found.");
                return NotFound("User with credentials is not found!");
            }
            _logger.LogInformation($"[Login] [User: {userLoginDTO.Email}] Has logged in successfully.");
            return Ok(new { token = token });
        }
    }
}
