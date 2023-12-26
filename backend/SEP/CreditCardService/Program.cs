using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using shared;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardService
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var paymentRequest = JsonConvert.DeserializeObject<PaymentRequest>(message);

                        var bankUrl = "https://localhost:7172/api/PSP/process-payment";
                        var httpClient = new HttpClient();
                        var jsonRequest = JsonConvert.SerializeObject(paymentRequest);
                        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                        var httpResponse = httpClient.PostAsync(bankUrl, content);
                    };
                }
            }
        }
    }
}
