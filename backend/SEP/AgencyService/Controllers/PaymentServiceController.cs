using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Models;
using AgencyService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ocelot.Errors;
using Shared;

namespace AgencyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
        private readonly IPaymentServiceService _paymentServiceService;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentServiceService> _logger;

        public PaymentServiceController(IPaymentServiceService paymentServiceService, IMapper mapper, ILogger<PaymentServiceService> logger)
        {
            _paymentServiceService = paymentServiceService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-payment-services/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var user = JwtDecode.DecodeToken(authorizationHeader);

            _logger.LogInformation($"[GetAll] [User: {user}] - Function is called.");

            var paymentServices = await _paymentServiceService.GetAll(id);
            if (paymentServices == null)
            {
                _logger.LogError($"[GetAll] [User: {user}] - PaymentServices of agency with id {id} does not exist!");
                return NotFound($"PaymentServices of agency with id {id} does not exist!");
            }

            _logger.LogInformation($"[GetAll] [User: {user}] - Function is completed successfully.");
            return Ok(paymentServices);
        }

        [HttpPost("create-payment-service")]
        public async Task<IActionResult> CreatePaymentService(CreatePaymentServiceDto paymentServiceDto, int agencyId)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var user = JwtDecode.DecodeToken(authorizationHeader);

            _logger.LogInformation($"[CreatePaymentService] [User: {user}] - Function is called.");

            var item = await _paymentServiceService.CreatePaymentServiceDto(paymentServiceDto, agencyId);
            if (item == null)
            {
                _logger.LogError($"[CreatePaymentService] [User: {user}] - Error while creating PaymentService!");
                return NotFound($"Error while creating PaymentService!");
            }

            _logger.LogInformation($"[CreatePaymentService] [User: {user}] - Function is completed successfully.");

            PaymentServiceDto retValue = new PaymentServiceDto();
            retValue = _mapper.Map<PaymentServiceDto>(item);
            return Ok(retValue);
        }

        [HttpPut("subscribe-payment-service")]
        public async Task<IActionResult> SubscribePaymentService([FromBody] List<PaymentServiceDto> paymentServicesDto, int agencyId)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var user = JwtDecode.DecodeToken(authorizationHeader);

            _logger.LogInformation($"[SubscribePaymentService] [User: {user}] - Function is called.");

            var items = await _paymentServiceService.SubscribePaymentService(paymentServicesDto, agencyId);
            if (items == null)
            {

                _logger.LogError($"[SubscribePaymentService] [User: {user}] - Error while subscribing PaymentService!");
                return NotFound($"Error while subscribing PaymentService!");
            }

            _logger.LogInformation($"[SubscribePaymentService] [User: {user}] - Function is completed successfully.");

            return Ok(items);
        }

    }
}
