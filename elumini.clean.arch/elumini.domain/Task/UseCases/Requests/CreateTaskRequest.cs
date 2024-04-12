namespace elumini.domain.Task.UseCases.Requests;
public class CreateTaskRequest
{
    public string Name { get; set; }
    public string Description { get; set; }        
    public DateTime EndAt { get; set; }
}
