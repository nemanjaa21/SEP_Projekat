using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared;

namespace AgencyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOfferController : ControllerBase
    {
        private readonly IServiceOfferService _serviceOfferService;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceOfferService> _logger;

        public ServiceOfferController(IServiceOfferService serviceOfferService, IMapper mapper, ILogger<ServiceOfferService> logger)
        {
            _serviceOfferService = serviceOfferService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOffer(int id)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var user = JwtDecode.DecodeToken(authorizationHeader);

            _logger.LogInformation($"[GetServiceOffer] [User: {user}] - Function is called.");

            var serviceOffer = await _serviceOfferService.GetServiceOfferById(id);
            if (serviceOffer == null)
            {
                _logger.LogError($"[GetServiceOffer] [User: {user}] - Service offer with id {id} does not exist!");
                return NotFound($"Service offer with id {id} does not exist!");
            }

            _logger.LogInformation($"[GetServiceOffer] [User: {user}] - Function is completed successfully.");

            return Ok(_mapper.Map<ServiceOfferDto>(serviceOffer));
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceOffer(Dictionary<int, bool> ids)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var user = JwtDecode.DecodeToken(authorizationHeader);

            _logger.LogInformation($"[CreateServiceOffer] [User: {user}] - Function is called.");

            var serviceOffer = _mapper.Map<ServiceOfferDto>(await _serviceOfferService.CreateServiceOffer(ids));

            _logger.LogInformation($"[CreateServiceOffer] [User: {user}] - Function is completed successfully.");
            return Ok(serviceOffer);
        }
    }
}
