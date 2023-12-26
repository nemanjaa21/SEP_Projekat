using Microsoft.AspNetCore.Mvc.Diagnostics;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using shared;
using System.Net.Http;
using System.Text;

namespace CardPaymentService.RabbitMQ
{
    public class RabbitMqUtil : IRabbitMqUtil
    {
        private readonly IConfiguration _configuration;
        private string bankUrl;
        private string queueName;
        public RabbitMqUtil(IConfiguration configuration)
        {
            _configuration = configuration;
            queueName = _configuration["QUEUE_NAME"];
            bankUrl = _configuration["BANKURL"];
        }
        public void SendResponseToPSP(string routingKey, string eventData)
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
            channel.BasicPublish(exchange: "return_logs", routingKey: routingKey, basicProperties: null, body: body);
        }

        public async Task ListenMessageQueue(IModel channel, string routingKey, CancellationToken cancellationToken)
        {
            var bankUrl = _configuration["BANKURL"];
            string? responseBody = "";
            var _httpClient = new HttpClient();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                PaymentRequest? paymentRequest = JsonConvert.DeserializeObject<PaymentRequest>(body);

                string jsonRequest = JsonConvert.SerializeObject(paymentRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.PostAsync(bankUrl, content);

                responseBody = await httpResponse.Content.ReadAsStringAsync();
                SendResponseToPSP("Credit_Card_Return", responseBody);
            };

            channel.BasicConsume(queue: "Credit_Card_Payment", autoAck: true, consumer: consumer);
            await Task.CompletedTask;
        }
    }
}
