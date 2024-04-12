using elumini.domain.Task;
using elumini.domain.TaskStatus;
using elumini.infra.mssql.Context;
using Microsoft.EntityFrameworkCore;
using elumini.domain.Task.Repositories;

namespace elumini.infra.mssql.Repositories;

public class TaskRepository : ITaskRepository
{    
    private readonly EfSqlContext _efContext;

    public TaskRepository(EfSqlContext efContext)
    {        
        _efContext = efContext;
    }

    public async Task<Tasks> CreateAsync(Tasks tasks)
    {
        _efContext.Task.Add(tasks);

        await _efContext.SaveChangesAsync(); 
        
        return tasks;
    }

    public async Task<bool> DeleteByIdAsync(int taskId)
    {
        var taskToRemove = await _efContext.Task.FirstOrDefaultAsync(n => n.Id == taskId);
        _efContext.Remove(taskToRemove!);
        
        return await _efContext.SaveChangesAsync() > 0;
    }

    public async Task<Tasks?> GetByIdAsync(int taskId)
    {
        return await _efContext.Task.Include("Status").FirstOrDefaultAsync(n => n.Id == taskId);
    }

    public async Task<IEnumerable<Tasks>> ListAllAsync()
    {
        return await _efContext.Task.Include("Status").ToListAsync();
    }

    public async Task<bool> UpdateAsync(Tasks tasks)
    {
        var existingTask = await _efContext.Task.FirstOrDefaultAsync(n => n.Id == tasks.Id);

        if (existingTask == null)
        {
            throw new Exception("Task not found");
        }

        return await _efContext.SaveChangesAsync() > 0;
    }
}
