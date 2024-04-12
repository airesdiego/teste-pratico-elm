using elumini.domain.Task;
using elumini.domain.Common;
using Microsoft.AspNetCore.Http;
using elumini.domain.Task.UseCases;
using Microsoft.Extensions.Logging;
using elumini.domain.Task.Repositories;
using elumini.application.Task.Validators;
using elumini.domain.Task.UseCases.Requests;
using elumini.domain.TaskStatus.Repositories;

namespace elumini.application.Task.UseCases;

public class UpdateTaskUseCase : IUpdateTaskUseCase
{
    private readonly ILogger<UpdateTaskUseCase> _logger;
    private readonly IStatusRepository _statusRepository;
    private readonly ITaskRepository _taskRepository;
    private static readonly UpdateTaskValidator _updateTaskValidator = new();

    public UpdateTaskUseCase(
        ILogger<UpdateTaskUseCase> logger,
        IStatusRepository statusRepository,
        ITaskRepository taskRepository)
    {
        _logger = logger;
        _statusRepository = statusRepository;
        _taskRepository = taskRepository;
    }

    public async Task<ApiResponse<Tasks>> ExecuteAsync(int taskId, UpdateTaskRequest request)
    {
        _logger.LogInformation($"{nameof(UpdateTaskUseCase)} => Begin execute...");

        try
        {
            var validations = await _updateTaskValidator.ValidateAsync(request);

            if (!validations.IsValid)
                return new ApiResponse<Tasks>(validations);

            var editTasks = await _taskRepository.GetByIdAsync(taskId);

            if (editTasks is null)
                return new ApiResponse<Tasks>(false, StatusCodes.Status202Accepted, new NotificationDto("1", "Not found task"));

            FillUpdateTask(request, editTasks);

            var result = await _taskRepository.UpdateAsync(editTasks);

            return new ApiResponse<Tasks>(result, StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {nameof(UpdateTaskUseCase)} : {ex.Message}");
            return new ApiResponse<Tasks>(false, StatusCodes.Status500InternalServerError);

        }
    }

    private void FillUpdateTask(UpdateTaskRequest request, Tasks editTask) 
    {
        editTask.Name = request.Name;
        editTask.Description = request.Description;
        editTask.EndAt = request.EndAt;
    }
}
