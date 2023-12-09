using AgencyService.DTO;
using AgencyService.Interfaces;
using AutoMapper;
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

        public ServiceOfferItemController(IServiceOfferItemService serviceOfferItemService, IMapper mapper)
        {
                _serviceOfferItemService = serviceOfferItemService;
                _mapper = mapper;
        }

        [HttpGet("get-all-service-offer-item")]
        public async Task<IActionResult> GetAllServiceOfferItem()
        {
            var items = await _serviceOfferItemService.GetAllServiceOfferItem();
            List<ServiceOfferItemDto> itemsDto = _mapper.Map<List<ServiceOfferItemDto>>(items);
            return Ok(itemsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOfferItem(int id)
        {
            var item = await _serviceOfferItemService.GetServiceOfferItemById(id);
            if(item == null)
            {
                return NotFound($"Item with id {id} does not exist!");
            }
            ServiceOfferItemDto serviceOfferItemDto = new ServiceOfferItemDto();
            serviceOfferItemDto = _mapper.Map<ServiceOfferItemDto>(item);
            return Ok(serviceOfferItemDto);
        }
    }
}
