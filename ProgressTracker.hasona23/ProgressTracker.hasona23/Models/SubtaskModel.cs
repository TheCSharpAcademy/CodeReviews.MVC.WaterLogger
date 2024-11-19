namespace ProgressTracker.hasona23.Models;

public class SubtaskModel
{
    public int Id { get; set; }
    public int ProgressId { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

}