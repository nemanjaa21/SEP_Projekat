using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPalPaymentService.Interfaces;

namespace PayPalPaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalPaymentController : ControllerBase
    {
        private IPayPalPaymentService _payPalPaymentService;
        public PayPalPaymentController(IPayPalPaymentService payPalPaymentService)
        {
            _payPalPaymentService = payPalPaymentService;
        }

        [HttpGet("payment")]
        public async Task<IActionResult> MakePayment()
        {
            string retVal = await _payPalPaymentService.MakePayment();
            return Ok(retVal);
        }
    }
}
