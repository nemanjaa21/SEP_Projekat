
using RabbitMQ.Client;
using shared;

namespace PaymentServiceProvider.RabbitMQ
{
    public interface IRabbitMqUtil
    {
        Task PublishMessageQueue(string routingKey, string eventData);
        Task<PaymentResponse> ListenMessageQueue(string queueName);
    }
}