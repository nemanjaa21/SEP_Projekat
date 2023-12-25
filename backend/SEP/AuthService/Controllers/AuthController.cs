using AuthService.DTO;
using AuthService.Interfaces;
using AuthService.Models;
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
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            User user = _mapper.Map<User>(userLoginDTO);
            string token = await _authService.Login(user);
            if (string.IsNullOrEmpty(token))
            {
                return NotFound("User with credentials is not found!");
            }
            return Ok(new { token = token });
        }
    }
}
