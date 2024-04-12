namespace elumini.domain.Task.UseCases.Responses;
public class TasksResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime EndAt { get; set; }
    public StatusResponse Status { get; set; }
}

public class StatusResponse 
{
    public int Id { get; set; }
    public string Name { get; set; }
}
