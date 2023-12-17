using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentCardCenterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCCControler : ControllerBase
    {
        [HttpPost]
        async Task <IActionResult> ToIssuerBank()
        {
            return Ok("TEST");
        }
    }
}
