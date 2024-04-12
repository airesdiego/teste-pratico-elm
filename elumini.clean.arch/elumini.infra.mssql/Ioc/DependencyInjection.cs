using System.Data.SqlClient;
using elumini.infra.mssql.Context;
using Microsoft.EntityFrameworkCore;
using elumini.domain.Task.Repositories;
using elumini.infra.mssql.Repositories;
using Microsoft.Extensions.Configuration;
using elumini.domain.TaskStatus.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace elumini.infra.mssql.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrctureLayerMssqlServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDapperConfiguration(configuration);
        services.AddEfConfiguration(configuration);
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();

        return services;
    }

    private static void AddDapperConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DapperSqlContext>();
        var constr = configuration.GetConnectionString("SqlServer");
        services.AddScoped<SqlConnection>(sp => new SqlConnection(constr));
    }

    private static void AddEfConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EfSqlContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("SqlServer")));
    }
}
