using BitcoinPaymentService.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("payment")] //ne koristi se
        public async Task<IActionResult> MakePayment()
        {
            string retVal = await _bitcoinPaymentService.MakePayment();
            return Ok(retVal);
        }


        [HttpPost("ethereum/create/{id}")]
        public async Task<IActionResult> CreateEthereumPayment(int id)
        {
            //int userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            var response = await _bitcoinPaymentService.CreateEthereumPayment(id, 3);
            return Ok(response);
        }

        [HttpGet("ethereum/check/{hash}")]
        public async Task<IActionResult> CheckEthereumPayment(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new Exception("Hash is required");
            await _bitcoinPaymentService.CheckEthereumPayment(hash);
            return Ok();
        }

        [HttpGet("ethereum/cancel/{id}")]
        public async Task<IActionResult> CheckEthereumPayment(int id)
        {
            await _bitcoinPaymentService.CancelEthereumPayment(id);
            return Ok();
        }
    }
}
