using System.Net.Mime;
using elumini.domain.Common;
using Microsoft.AspNetCore.Mvc;
using elumini.domain.Task.UseCases;
using elumini.domain.Task.UseCases.Requests;
using elumini.domain.Task.UseCases.Responses;

namespace elumini.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ICreateTaskUseCase _createTaskUseCase;
        private readonly IUpdateTaskUseCase _updateTaskUseCase;
        private readonly IDeleteUseCase _deleteUseCase;
        private readonly IListTaskUseCase _listTaskUseCase;

        public TasksController(ILogger<TasksController> logger,
            ICreateTaskUseCase createTaskUseCase,
            IUpdateTaskUseCase updateTaskUseCase,
            IDeleteUseCase deleteUseCase,
            IListTaskUseCase listTaskUseCase)
        {
            _logger = logger;
            _createTaskUseCase = createTaskUseCase;
            _updateTaskUseCase = updateTaskUseCase;
            _deleteUseCase = deleteUseCase;
            _listTaskUseCase = listTaskUseCase;
        }

        [HttpPost(Name = "Create")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            _logger.LogInformation($"Controller {nameof(TasksController)}");
            _logger.LogInformation($"Method {nameof(Create)}");

            var response = await _createTaskUseCase.ExecuteAsync(request);

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPut]
        [Route("{taskId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(int taskId, [FromBody] UpdateTaskRequest request)
        {
            _logger.LogInformation($"Controller {nameof(TasksController)}");
            _logger.LogInformation($"Method {nameof(Edit)}");

            var response = await _updateTaskUseCase.ExecuteAsync(taskId, request);

            if (response.StatusCode == 204)
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            else
                return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpDelete]
        [Route("{taskId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(int taskId)
        {
            _logger.LogInformation($"Controller {nameof(TasksController)}");
            _logger.LogInformation($"Method {nameof(Remove)}");

            var response = await _deleteUseCase.ExecuteAsync(taskId);   

            if(response.StatusCode == 204)
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            else
                return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpGet]        
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TasksResponse>>), StatusCodes.Status200OK)]        
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Controller {nameof(TasksController)}");
            _logger.LogInformation($"Method {nameof(GetAll)}");

            var response = await _listTaskUseCase.ExecuteAsync();

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
