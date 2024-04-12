using elumini.domain.Common;
using t = System.Threading.Tasks;

namespace elumini.domain.Task.UseCases;
public interface IDeleteUseCase
{
    t.Task<ApiResponse<Tasks>> ExecuteAsync(int taskId);
}
