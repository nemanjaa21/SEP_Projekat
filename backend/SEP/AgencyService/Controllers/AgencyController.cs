using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyService _agencyService;
        private readonly IMapper _mapper;
        private ILogger<Service.AgencyService> _logger;

        public AgencyController(IAgencyService agencyService, IMapper mapper, ILogger<Service.AgencyService> logger)
        {
            _agencyService = agencyService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgencyById(int id)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAgencyById] [User: {user}] - Function is called.");

            var agency = await _agencyService.GetAgencyById(id);
            if (agency == null)
            {
                _logger.LogError($"[GetAgencyById] [User: {user}] -Agency with id {id} does not exist!");
                return NotFound($"Agency with id {id} does not exist!");
            }

            _logger.LogInformation($"[GetAgencyById] [User: {user}] - Function is completed successfully.");
            return Ok(_mapper.Map<AgencyDto>(agency));
        }
    }
}
