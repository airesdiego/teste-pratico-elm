using elumini.domain.Task;
using elumini.domain.Task.UseCases.Requests;

namespace elumini.application.Task.Mappers;
public static class CreateTaskRequestParser
{
    public static Tasks ConvertToTasks(CreateTaskRequest request) 
    {
        return new Tasks
        {
            Description = request.Description,
            Name = request.Name,
            EndAt = request.EndAt
        };
    }
}
