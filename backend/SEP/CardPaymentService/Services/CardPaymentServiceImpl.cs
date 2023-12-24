using CardPaymentService.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using shared;
using System.Text;

namespace CardPaymentService.Services
{
    public class CardPaymentServiceImpl : ICardPaymentService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        private string queueName;
        private string bankUrl;
        public CardPaymentServiceImpl(IConfiguration configuration)
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            this._configuration = configuration;

            queueName = _configuration["QUEUE_NAME"];
            bankUrl = _configuration["BANKURL"];

        }
        public void MakePayment()
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                PaymentRequest? paymentRequest = JsonConvert.DeserializeObject<PaymentRequest>(message);

                PaymentResponse paymentResponse = new PaymentResponse(bankUrl, null);

                var props = ea.BasicProperties;
                var replyProps = _channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                var responseMessage = JsonConvert.SerializeObject(paymentResponse);
                var responseBytes = Encoding.UTF8.GetBytes(responseMessage);

                _channel.BasicPublish(exchange: "",
                                     routingKey: props.ReplyTo,
                                     basicProperties: replyProps,
                                     body: responseBytes);
            };
        }
    }
}
