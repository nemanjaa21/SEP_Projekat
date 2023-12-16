using AutoMapper.Internal;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shared;
using BankService.Interfaces;

namespace BankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PSPController : ControllerBase
    {
        private readonly IPSPService _pspService;
        public PSPController(IPSPService pspService)
        {
            _pspService = pspService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            PaymentResponse paymentResponse = await _pspService.ProcessPayment(paymentRequest);

            return Ok(paymentResponse);
        }
    }
}
