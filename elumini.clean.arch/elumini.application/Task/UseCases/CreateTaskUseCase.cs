using elumini.domain.Task;
using elumini.domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using elumini.domain.Task.UseCases;
using elumini.domain.Task.Repositories;
using elumini.application.Common.Enums;
using elumini.application.Task.Mappers;
using elumini.application.Task.Validators;
using elumini.domain.Task.UseCases.Requests;
using elumini.domain.TaskStatus.Repositories;

namespace elumini.application.Task.UseCases;

public class CreateTaskUseCase : ICreateTaskUseCase
{
    private readonly ILogger<CreateTaskUseCase> _logger;
    private readonly IStatusRepository _statusRepository;
    private readonly ITaskRepository _taskRepository;
    private static readonly CreateTaskValidator _createTaskValidator = new ();

    public CreateTaskUseCase(
        ILogger<CreateTaskUseCase> logger,
        IStatusRepository statusRepository,
        ITaskRepository taskRepository)
    {
        _logger = logger;
        _statusRepository = statusRepository;
        _taskRepository = taskRepository;
    }

    public async Task<ApiResponse<Tasks>> ExecuteAsync(CreateTaskRequest request)
    {
        _logger.LogInformation($"{nameof(CreateTaskUseCase)} => Begin execute...");

        try
        {
            var validations = await _createTaskValidator.ValidateAsync(request);

            if (!validations.IsValid)
                return new ApiResponse<Tasks>(validations);

            var status = await _statusRepository.GetByIdAsync((int)StstusType.New);

            var tasks = CreateTaskRequestParser.ConvertToTasks(request);
            tasks.CreateAt = DateTime.Now;
            tasks.Status = status!;

            var result = await _taskRepository.CreateAsync(tasks);

            return new ApiResponse<Tasks>(true, StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {nameof(CreateTaskUseCase)} : {ex.Message}");
            return new ApiResponse<Tasks>(false, StatusCodes.Status500InternalServerError);
        }
    }
}
