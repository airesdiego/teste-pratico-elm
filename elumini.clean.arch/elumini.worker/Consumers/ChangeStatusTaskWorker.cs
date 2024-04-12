using elumini.worker.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace elumini.worker.Consumers;

public class ChangeStatusTaskWorker : BackgroundService
{
    private readonly ILogger<ChangeStatusTaskWorker> _logger;
    private readonly RabbitMqConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public ChangeStatusTaskWorker(ILogger<ChangeStatusTaskWorker> logger,
        IOptions<RabbitMqConfiguration> option,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _configuration = option.Value;
        _serviceProvider = serviceProvider;

        var factory = new ConnectionFactory
        {
            HostName = _configuration.Host
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
                    queue: _configuration.Queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var message = JsonConvert.DeserializeObject<TaskStatusInputModel>(contentString);


            NotifyAndUpdateStatus(message);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(_configuration.Queue, false, consumer);

        return Task.CompletedTask;
    }

    private void NotifyAndUpdateStatus(TaskStatusInputModel message) 
    {
        _logger.LogInformation($"Ack: { JsonConvert.SerializeObject(message) }");
    }
}
