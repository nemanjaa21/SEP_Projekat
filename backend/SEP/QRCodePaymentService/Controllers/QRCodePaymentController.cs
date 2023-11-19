using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCodePaymentService.Interfaces;

namespace QRCodePaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodePaymentController : ControllerBase
    {
        private IQRCodePaymentService _QRCodePaymentService;
        public QRCodePaymentController(IQRCodePaymentService QRCodePaymentService)
        {
            _QRCodePaymentService = QRCodePaymentService;
        }

        [HttpGet("payment")]
        public async Task<IActionResult> MakePayment()
        {
            string retVal = await _QRCodePaymentService.MakePayment();
            return Ok(retVal);
        }
    }
}
