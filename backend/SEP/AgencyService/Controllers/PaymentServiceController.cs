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
    public class PaymentServiceController : ControllerBase
    {
        private readonly IPaymentServiceService _paymentServiceService;
        private readonly IMapper _mapper;

        public PaymentServiceController(IPaymentServiceService paymentServiceService, IMapper mapper)
        {
            _paymentServiceService = paymentServiceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            var paymentServices = await _paymentServiceService.GetAll(id);
            if (paymentServices == null)
            {
                return NotFound($"PaymentServices of agency with id {id} does not exist!");
            }
            return Ok(paymentServices);
        }
    }
}
