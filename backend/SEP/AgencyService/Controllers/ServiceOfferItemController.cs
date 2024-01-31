using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOfferItemController : ControllerBase
    {
        private readonly IServiceOfferItemService _serviceOfferItemService;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceOfferItemService> _logger;

        public ServiceOfferItemController(IServiceOfferItemService serviceOfferItemService, IMapper mapper, ILogger<ServiceOfferItemService> logger)
        {
            _serviceOfferItemService = serviceOfferItemService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-all-service-offer-item")]
        public async Task<IActionResult> GetAllServiceOfferItem()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllServiceOfferItem] [User: {user}] - Function is called.");

            var items = await _serviceOfferItemService.GetAllServiceOfferItem();
            List<ServiceOfferItemDto> itemsDto = _mapper.Map<List<ServiceOfferItemDto>>(items);


            _logger.LogInformation($"[GetAllServiceOfferItem] [User: {user}] - Function is completed successfully.");

            return Ok(itemsDto);
        }

        [HttpGet("get-service-offer-item/{id}")]
        public async Task<IActionResult> GetServiceOfferItem(int id)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetServiceOfferItem] [User: {user}] - Function is called.");

            var item = await _serviceOfferItemService.GetServiceOfferItemById(id);
            if(item == null)
            {
                _logger.LogError($"[GetServiceOffer] [User: {user}] - Item with id {id} does not exist!");
                return NotFound($"Item with id {id} does not exist!");
            }
            ServiceOfferItemDto serviceOfferItemDto = new ServiceOfferItemDto();
            serviceOfferItemDto = _mapper.Map<ServiceOfferItemDto>(item);

            _logger.LogInformation($"[GetServiceOfferItem] [User: {user}] - Function is completed successfully.");

            return Ok(serviceOfferItemDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceOfferItem(CreateServiceOfferItemDto serviceOfferItemDto, int agencyId)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[CreateServiceOfferItem] [User: {user}] - Function is called.");

            var item = await _serviceOfferItemService.CreateServiceOfferItem(serviceOfferItemDto, agencyId);
            if (item == null)
            {
                _logger.LogError($"[CreateServiceOfferItem] [User: {user}] - Error while creating ServiceOfferItem!");
                return NotFound($"Error while creating ServiceOfferItem!");
            }
            ServiceOfferItemDto retValue = new ServiceOfferItemDto();
            retValue = _mapper.Map<ServiceOfferItemDto>(item);

            _logger.LogInformation($"[CreateServiceOfferItem] [User: {user}] - Function is completed successfully.");

            return Ok(retValue);
        }
    }
}
