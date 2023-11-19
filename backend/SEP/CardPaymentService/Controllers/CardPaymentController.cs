using CardPaymentService.Interfaces;
using CardPaymentService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardPaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardPaymentController : ControllerBase
    {
        private ICardPaymentService _cardPaymentService;
        public CardPaymentController(ICardPaymentService cardPaymentService)
        {
            _cardPaymentService = cardPaymentService;
        }

        [HttpGet("payment")]
        public async Task<IActionResult> MakePayment()
        {
            string retVal = await _cardPaymentService.MakePayment();
            return Ok(retVal);
        }
    }
}
