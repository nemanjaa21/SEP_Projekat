﻿using QRCodePaymentService.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using shared;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace QRCodePaymentService.Services
{
    public class QRCodePaymentServiceImpl : IQRCodePaymentService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private string queueName;
        private string bankUrl;
        public QRCodePaymentServiceImpl(IConfiguration configuration)
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _configuration = configuration;
            _httpClient = new HttpClient();

            queueName = _configuration["QUEUE_NAME"];
            bankUrl = _configuration["BANKURL"];

        }
        public void MakePayment()
        {
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
