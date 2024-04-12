using elumini.domain.Common;
using elumini.domain.Task.UseCases.Responses;

namespace elumini.domain.Task.UseCases;

public interface IListTaskUseCase
{
    Task<ApiResponse<IEnumerable<TasksResponse>>> ExecuteAsync();
}
