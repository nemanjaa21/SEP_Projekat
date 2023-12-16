using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceProvider.Dto;
using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        private readonly IMapper _mapper;

        public MerchantController(IMerchantService merchantService, IMapper mapper)
        {
            _merchantService = merchantService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMerchant(RegisterMerchantDTO registerMerchant)
        {
            Merchant merchant = _mapper.Map<Merchant>(registerMerchant);
            string ApiKey = await _merchantService.RegisterMerchant(merchant);
            return Ok(ApiKey);
        }
    }
}
