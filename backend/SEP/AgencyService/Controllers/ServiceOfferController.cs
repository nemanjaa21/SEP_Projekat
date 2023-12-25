using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AgencyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOfferController : ControllerBase
    {
        private readonly IServiceOfferService _serviceOfferService;
        private readonly IMapper _mapper;

        public ServiceOfferController(IServiceOfferService serviceOfferService, IMapper mapper)
        {
            _serviceOfferService = serviceOfferService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOffer(int id)
        {
            var serviceOffer = await _serviceOfferService.GetServiceOfferById(id);
            if (serviceOffer == null)
            {
                return NotFound($"Service offer with id {id} does not exist!");
            }
            return Ok(_mapper.Map<ServiceOfferDto>(serviceOffer));
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceOffer(Dictionary<int, bool> ids)
        {
            var serviceOffer = _mapper.Map<ServiceOfferDto>(await _serviceOfferService.CreateServiceOffer(ids));            
            return Ok(serviceOffer);
        }
    }
}
