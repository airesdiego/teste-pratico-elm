namespace elumini.worker.Models;

public class TaskStatusInputModel
{
    public int TaskId { get; set; }
    public int TaskStatusId { get; set; }
    public string Message { get; set; }
}
