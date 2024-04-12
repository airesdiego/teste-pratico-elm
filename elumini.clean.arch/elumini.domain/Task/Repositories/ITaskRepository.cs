namespace elumini.domain.Task.Repositories;

public interface ITaskRepository
{
    Task<Tasks> CreateAsync(Tasks tasks);
    Task<bool> UpdateAsync(Tasks tasks);
    Task<Tasks?> GetByIdAsync(int taskId);
    Task<bool> DeleteByIdAsync(int taskId);
    Task<IEnumerable<Tasks>> ListAllAsync();
}
