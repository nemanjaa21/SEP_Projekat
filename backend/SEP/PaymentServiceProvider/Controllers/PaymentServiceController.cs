using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceProvider.Dto;
using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;
using shared;

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
            List<PaymentServiceDTO> paymentServiceDto = new List<PaymentServiceDTO>();
            var paymentMethods = await _paymentService.GetAllPaymentMethods();
            paymentServiceDto = _mapper.Map<List<PaymentServiceDTO>>(paymentMethods);
            return Ok(paymentServiceDto);
        }

        [HttpPost("process-payment")]
        public async Task<IActionResult> ProcessPayment(PSPRequest pspRequest, string apiKey)
        {
            PaymentResponse paymentResponse = await _paymentService.ProcessPayment(pspRequest, apiKey);

            return Ok(paymentResponse);
        }
    }
}
