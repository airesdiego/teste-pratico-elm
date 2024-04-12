using elumini.infra.mssql.Ioc;
using elumini.worker.Consumers;
using elumini.worker.Senders;

namespace elumini.worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json", false);

                }).ConfigureServices((builderContext, services) =>
                {
                    services.Configure<RabbitMqConfiguration>(builderContext.Configuration.GetSection("RabbitMqConfig"));
                    services.AddInfrastrctureLayerMssqlServices(builderContext.Configuration);
                    services.AddHostedService<CheckTasksWorker>();
                    services.AddHostedService<ChangeStatusTaskWorker>();

                })
                .Build();

            host.Run();
        }
    }
}