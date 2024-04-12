using elumini.domain.Task.UseCases;
using elumini.application.Task.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace elumini.application;

public static class DependencyInjection
{
    public static IServiceCollection InjectUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
        services.AddScoped<IDeleteUseCase, DeleteUseCase>();
        services.AddScoped<IListTaskUseCase, ListTaskUseCase>();
        services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();

        return services;
    }
}
