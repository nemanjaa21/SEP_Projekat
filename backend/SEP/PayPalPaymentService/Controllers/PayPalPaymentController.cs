using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPalPaymentService.Dto;
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

        [HttpPost("payment")]
        public async Task<IActionResult> MakePayment(ServiceOfferDto offerDto)
        {
            string retVal = await _payPalPaymentService.MakePayment(offerDto);
            return Ok(retVal);
        }

        [HttpGet("payment/success/{id}")]
        public async Task<IActionResult> ConfirmPayPalPayment(int id, string paymentId, string token, string PayerID)
        {
            var result = await _payPalPaymentService.SuccessPayPalPayment(paymentId, PayerID, id);
            return Redirect("http://localhost:3000");
        }

        [HttpGet("payment/cancel/{id}")]
        public async Task<IActionResult> CancelPayPalPayment(int id)
        {

            //await _payPalPaymentService.CancelPayPalPayment(id);
            return Redirect("http://localhost:3000");
        }
    }
}
