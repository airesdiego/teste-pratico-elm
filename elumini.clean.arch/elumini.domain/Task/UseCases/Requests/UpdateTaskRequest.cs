namespace elumini.domain.Task.UseCases.Requests;

public class UpdateTaskRequest
{    
    public string Name { get; set; }
    public string Description { get; set; }
    public int StatusId { get; set; }
    public DateTime EndAt { get; set; }
}
