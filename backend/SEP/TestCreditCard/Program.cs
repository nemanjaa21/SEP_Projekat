using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace YourConsoleAppNamespace
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var _channel = connection.CreateModel();
            var queueName = "Credit_Card_Payment";
            var bankUrl = "https://localhost:7172/api/PSP/process-payment";

            var httpClient = new HttpClient();

            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                PaymentRequest? paymentRequest = JsonConvert.DeserializeObject<PaymentRequest>(message);

                string jsonRequest = JsonConvert.SerializeObject(paymentRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.PostAsync(bankUrl, content);

                string? responseBody = await httpResponse.Content.ReadAsStringAsync();
                PaymentResponse? paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(responseBody);

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
