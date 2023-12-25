using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using shared;
using System.Text;

namespace PaymentServiceProvider.RabbitMQ
{
    public class RabbitMqUtil : IRabbitMqUtil
    {
        public async Task PublishMessageQueue(string routingKey, string eventData)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "direct_logs", routingKey: routingKey, basicProperties: null, body: body);

            await Task.CompletedTask;
        }
        public async Task<PaymentResponse> ListenMessageQueue()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var paymentResponse = new PaymentResponse();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(body);
            };

            channel.BasicConsume(queue: "Credit_Card_Return", autoAck: true, consumer: consumer);

            await Task.CompletedTask;

            return paymentResponse;
        }
    }
}
