using Newtonsoft.Json;
using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;
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
        private readonly ConnectionFactory factory;
        private readonly IConnection connection;
        private readonly IModel channel;
        HttpClient _httpClient;

        public PaymentServiceImpl(IUnitOfWork unitOfWork, IMerchantService merchantService) 
        { 
            _unitOfWork = unitOfWork;
            _merchantService = merchantService;
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
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

            PaymentResponse? response = SendPaymentRequestAndGetResponse(paymentRequest, paymentType.Name!);

            return response!;
        }

        private PaymentResponse SendPaymentRequestAndGetResponse(PaymentRequest request, string paymentTypeName)
        {
            channel.QueueDeclare(queue: paymentTypeName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var replyQueueName = channel.QueueDeclare().QueueName;

            var consumer = new EventingBasicConsumer(channel);
            var correlationId = Guid.NewGuid().ToString();

            var props = channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            var message = JsonConvert.SerializeObject(request);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                             routingKey: paymentTypeName,
                             basicProperties: props,
                             body: body);

            PaymentResponse? response = null;
            var responseEvent = new ManualResetEvent(false);

            consumer.Received += (model, ea) =>
            {
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    var responseMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                    response = JsonConvert.DeserializeObject<PaymentResponse>(responseMessage);
                    responseEvent.Set();
                }
            };

            channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);

            responseEvent.WaitOne();

            return response!;
        }
    }
}
