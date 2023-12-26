
using RabbitMQ.Client;

namespace QRCodePaymentService.RabbitMQ
{
    public class RabbitMqService : BackgroundService
    {
        private readonly IRabbitMqUtil _rabbitMqUtil;
        private IModel _channel;
        private IConnection _connection;
        private IConfiguration _configuration;
        private string queueName;
        public RabbitMqService(IRabbitMqUtil rabbitMqUtil, IConfiguration configuration)
        {
            _rabbitMqUtil = rabbitMqUtil;
            _configuration = configuration;
            queueName = _configuration["QUEUE_NAME"];
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMqUtil.ListenMessageQueue(_channel, queueName, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
        }
    }
}
