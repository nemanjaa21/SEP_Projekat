using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentCardCenterService.DTO;
using PaymentCardCenterService.Interfaces;

namespace PaymentCardCenterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCCControler : ControllerBase
    {
        private readonly IPCCService _pccService;
        public PCCControler(IPCCService pccService)
        {
            _pccService = pccService;
        }

        [HttpPost("forward-to-issuer-bank")]
        public async Task <IActionResult> ToIssuerBank([FromBody] PCCRequestDTO pccRequestDTO)
        {
            try
            {
                PCCResponseDTO pccResponseDTO = await _pccService.ForwardToIssuerBank(pccRequestDTO);
                return Ok(pccResponseDTO);
            }
            catch (Exception)
            {
                return BadRequest(new PCCResponseDTO());
            }
        }
    }
}
