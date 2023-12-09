using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceProvider.Dto;
using PaymentServiceProvider.Interfaces;

namespace PaymentServiceProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentServiceController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            var paymentMethods = await _paymentService.GetAllPaymentMethods();
            List<PaymentServiceDto> paymentServiceDto = new List<PaymentServiceDto>();
            paymentServiceDto = _mapper.Map<List<PaymentServiceDto>>(paymentMethods);
            return Ok(paymentMethods);
        }
    }
}
