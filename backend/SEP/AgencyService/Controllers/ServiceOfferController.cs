using AgencyService.DTO;
using AgencyService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOfferController : ControllerBase
    {
        private readonly IServiceOfferItemService _serviceOfferItemService;
        private readonly IMapper _mapper;

        public ServiceOfferController(IServiceOfferItemService serviceOfferItemService, IMapper mapper)
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
    }
}
