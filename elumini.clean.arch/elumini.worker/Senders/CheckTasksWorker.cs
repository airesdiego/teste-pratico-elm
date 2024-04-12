using elumini.domain.Task.Repositories;
using elumini.domain.TaskStatus.Repositories;
using elumini.infra.mssql.Repositories;
using elumini.worker.Consumers;
using elumini.worker.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace elumini.worker.Senders;

public class CheckTasksWorker : BackgroundService
{
    private readonly ILogger<CheckTasksWorker> _logger;    
    private readonly RabbitMqConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly ConnectionFactory _factory;

    public CheckTasksWorker(ILogger<CheckTasksWorker> logger,
        IOptions<RabbitMqConfiguration> option,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _configuration = option.Value;
        _serviceProvider = serviceProvider;        

        _factory = new ConnectionFactory
        {
            HostName = _configuration.Host
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("CheckTasksWorker running at: {time}", DateTimeOffset.Now);

            // Create service scope
            using var scope = _serviceProvider.CreateScope();
            // Access scoped services like this:
            var taskRepository = scope.ServiceProvider.GetService<ITaskRepository>();

            var tasks = await taskRepository!.ListAllAsync();

            if(!tasks.Any())
                _logger.LogInformation("CheckTasksWorker running at: {time} - No messages", DateTimeOffset.Now);

            foreach ( var task in tasks ) 
            { 
                if(task.CreateAt > DateTime.Now || task.EndAt > DateTime.Now || task.Status.Id == 1)
                    SendMessage(new TaskStatusInputModel() { TaskId = task.Id, TaskStatusId = task.Status.Id });
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private void SendMessage(TaskStatusInputModel message) 
    {
        using (var connection = _factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _configuration.Queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var stringfiedMessage = JsonConvert.SerializeObject(message);
                var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: _configuration.Queue,
                    basicProperties: null,
                    body: bytesMessage);
            }
        }

    }
}
