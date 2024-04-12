using elumini.domain.Common;
using Microsoft.AspNetCore.Http;
using elumini.domain.Task.UseCases;
using Microsoft.Extensions.Logging;
using elumini.application.Task.Mappers;
using elumini.domain.Task.Repositories;
using elumini.domain.Task.UseCases.Responses;

namespace elumini.application.Task.UseCases;

public class ListTaskUseCase : IListTaskUseCase
{
    private readonly ILogger<ListTaskUseCase> _logger;
    private readonly ITaskRepository _taskRepository;

    public ListTaskUseCase(
        ILogger<ListTaskUseCase> logger,
        ITaskRepository taskRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;
    }

    public async Task<ApiResponse<IEnumerable<TasksResponse>>> ExecuteAsync()
    {
        _logger.LogInformation($"{nameof(ListTaskUseCase)} => Begin execute...");

        try
        {
            var listTasks = await _taskRepository.ListAllAsync();

            var result = ListTaskResponseParser.ConvertToTasksResponse(listTasks);

            return new ApiResponse<IEnumerable<TasksResponse>>(true, result, StatusCodes.Status200OK );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {nameof(ListTaskUseCase)} : {ex.Message}");
            return new ApiResponse<IEnumerable<TasksResponse>>(false, StatusCodes.Status500InternalServerError);
        }
    }
}
