using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
        private readonly IPaymentServiceService _paymentServiceService;
        private readonly IMapper _mapper;

        public PaymentServiceController(IPaymentServiceService paymentServiceService, IMapper mapper)
        {
            _paymentServiceService = paymentServiceService;
            _mapper = mapper;
        }

        [HttpGet("get-payment-services/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var paymentServices = await _paymentServiceService.GetAll(id);
            if (paymentServices == null)
            {
                return NotFound($"PaymentServices of agency with id {id} does not exist!");
            }
            return Ok(paymentServices);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentService(CreatePaymentServiceDto paymentServiceDto, int agencyId)
        {
            var item = await _paymentServiceService.CreatePaymentServiceDto(paymentServiceDto, agencyId);
            if (item == null)
            {
                return NotFound($"Error while creating PaymentService!");
            }
            PaymentServiceDto retValue = new PaymentServiceDto();
            retValue = _mapper.Map<PaymentServiceDto>(item);
            return Ok(retValue);
        }
    }
}
