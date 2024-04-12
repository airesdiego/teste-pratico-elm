using elumini.domain.TaskStatus;

namespace elumini.domain.Task;

public class Tasks
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime EndAt { get; set; }
    public virtual Status Status { get; set; }
}
