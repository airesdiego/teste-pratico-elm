using elumini.domain.Task;
using elumini.domain.Task.UseCases.Responses;

namespace elumini.application.Task.Mappers;

public static class ListTaskResponseParser
{
    public static TasksResponse ConvertToTasksResponse(Tasks tasks) 
    {
        return new TasksResponse
        {
            CreateAt = tasks.CreateAt,
            Description = tasks.Description,
            EndAt = tasks.EndAt,
            Id = tasks.Id,
            Name = tasks.Name,
            Status = new StatusResponse() { Id = tasks.Status.Id, Name = tasks.Status.Name }
        };
    }

    public static IEnumerable<TasksResponse> ConvertToTasksResponse(IEnumerable<Tasks> tasks)
    {
        var list = new List<TasksResponse>();

        if(tasks is null || !tasks.Any())
            return list;

        foreach (var task in tasks)
        { 
            list.Add(ConvertToTasksResponse(task));
        }

        return list;
    }
}
