namespace ProgressTracker.hasona23.Models;
public class ProgressModel
{
    public int Id { get; set; }
    public int Completed => SubTasks.Count(x => x.IsCompleted);
    public int Max => SubTasks.Count;
    public string Title { get; set; }
    public List<SubtaskModel> SubTasks { get; set; }

    public ProgressModel()
    {
        SubTasks = new();
    }
    public ProgressModel(int id, string title, params SubtaskModel[] subTasks)
    {
        Id = id;
        Title = title;
        SubTasks = subTasks.ToList() ?? [];
    }
}

