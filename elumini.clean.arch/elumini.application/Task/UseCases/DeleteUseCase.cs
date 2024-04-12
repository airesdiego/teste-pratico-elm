using elumini.domain.Task;
using elumini.domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using elumini.domain.Task.UseCases;
using elumini.domain.Task.Repositories;
using elumini.domain.TaskStatus.Repositories;

namespace elumini.application.Task.UseCases;

public class DeleteUseCase : IDeleteUseCase
{
    private readonly ILogger<DeleteUseCase> _logger;
    private readonly IStatusRepository _statusRepository;
    private readonly ITaskRepository _taskRepository;

    public DeleteUseCase(
        ILogger<DeleteUseCase> logger,
        IStatusRepository statusRepository,
        ITaskRepository taskRepository)
    {
        _logger = logger;
        _statusRepository = statusRepository;
        _taskRepository = taskRepository;
    }

    public async Task<ApiResponse<Tasks>> ExecuteAsync(int taskId)
    {
        _logger.LogInformation($"{nameof(DeleteUseCase)} => Begin execute...");

        try
        {
            var deleteTasks = await _taskRepository.GetByIdAsync(taskId);

            if (deleteTasks is null)
                return new ApiResponse<Tasks>(false, StatusCodes.Status202Accepted, new NotificationDto("1", "Not found task"));

            var result = await _taskRepository.DeleteByIdAsync(taskId);

            return new ApiResponse<Tasks>(result, result ? StatusCodes.Status204NoContent : StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {nameof(DeleteUseCase)} : {ex.Message}");
            return new ApiResponse<Tasks>(false, StatusCodes.Status500InternalServerError);
        }
    }
}
