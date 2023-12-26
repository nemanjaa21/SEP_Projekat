
using RabbitMQ.Client;

namespace CardPaymentService.RabbitMQ
{
    public interface IRabbitMqUtil
    {
        void SendResponseToPSP(string routingKey, string eventData);
        Task ListenMessageQueue(IModel channel, string routingKey, CancellationToken cancellationToken);
    }
}