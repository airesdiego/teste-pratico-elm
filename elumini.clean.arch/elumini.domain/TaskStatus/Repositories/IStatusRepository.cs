namespace elumini.domain.TaskStatus.Repositories;

public interface IStatusRepository
{
    Task<Status?> GetByIdAsync(int statusId);
    Task<IEnumerable<Status>> ListAllAsync();
}