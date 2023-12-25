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

        public AgencyController(IAgencyService agencyService, IMapper mapper)
        {
            _agencyService = agencyService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgencyById(int id)
        {
            var agency = await _agencyService.GetAgencyById(id);
            if (agency == null)
            {
                return NotFound($"Agency with id {id} does not exist!");
            }
            return Ok(_mapper.Map<AgencyDto>(agency));
        }
    }
}
