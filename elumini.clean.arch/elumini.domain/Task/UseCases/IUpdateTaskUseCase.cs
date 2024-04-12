using elumini.domain.Common;
using t = System.Threading.Tasks;
using elumini.domain.Task.UseCases.Requests;

namespace elumini.domain.Task.UseCases;
public interface IUpdateTaskUseCase
{
    t.Task<ApiResponse<Tasks>> ExecuteAsync(int taskId, UpdateTaskRequest request);
}
