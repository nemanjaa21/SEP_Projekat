using Newtonsoft.Json;
using Ocelot.RequestId;
using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;
using PaymentServiceProvider.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using shared;
using System.Text;

namespace PaymentServiceProvider.Services
{
    public class PaymentServiceImpl : IPaymentService
    {
        private IUnitOfWork _unitOfWork;
        private IMerchantService _merchantService;
        private IRabbitMqUtil _rabbitMqUtil;
        HttpClient _httpClient;

        public PaymentServiceImpl(IUnitOfWork unitOfWork, IMerchantService merchantService, IRabbitMqUtil rabbitMqUtil) 
        { 
            _unitOfWork = unitOfWork;
            _merchantService = merchantService;
            _rabbitMqUtil = rabbitMqUtil;
            _httpClient = new HttpClient();
        }

        public async Task<List<PaymentService>> GetAllPaymentMethods()
        {
            var paymentMethods = await _unitOfWork.PaymentServiceRepository.GetAll();
            return paymentMethods.ToList();
        }

        public async Task<PaymentService> GetPaymentService(int id)
        {
            var paymentService = await _unitOfWork.PaymentServiceRepository.Get(x => x.Id == id);
            if (paymentService == null)
                throw new Exception("Payment service isn't valid");

            return paymentService;
        }

        public async Task<PaymentResponse> ProcessPayment(PSPRequest pspRequest, string apiKey)
        {
            Random random = new Random();
            Merchant? merchant = await _merchantService.GetMercantByApiKey(apiKey);
            PaymentService? paymentType = await GetPaymentService(pspRequest.PaymentTypeId);
            long merchantOrderId = (long)(random.NextDouble() * 1000000);

            PaymentRequest paymentRequest = new PaymentRequest(merchant.MerchantId, merchant.MerchantPassword, pspRequest.Amount, merchantOrderId, DateTime.Now, "SUCCESS_URL", "FAIL_URL", "ERROR_URL");

            var eventData = JsonConvert.SerializeObject(paymentRequest);

            await _rabbitMqUtil.PublishMessageQueue(paymentType.Name!, eventData);

            PaymentResponse paymentResponse = await _rabbitMqUtil.ListenMessageQueue(paymentType.Name!);
            return paymentResponse;
        }
    }
}
