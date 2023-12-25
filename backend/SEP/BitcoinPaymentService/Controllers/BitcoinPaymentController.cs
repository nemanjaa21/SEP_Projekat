using BitcoinPaymentService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinPaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinPaymentController : ControllerBase
    {
        private IBitcoinPaymentService _bitcoinPaymentService;
        public BitcoinPaymentController(IBitcoinPaymentService bitcoinPaymentService)
        {
            _bitcoinPaymentService = bitcoinPaymentService;
        }

        [HttpGet("payment")]
        public async Task<IActionResult> MakePayment()
        {
            string retVal = await _bitcoinPaymentService.MakePayment();
            return Ok(retVal);
        }
    }
}
