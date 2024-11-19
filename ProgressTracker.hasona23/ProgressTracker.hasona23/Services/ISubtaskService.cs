using ProgressTracker.hasona23.Models;

namespace ProgressTracker.hasona23.Services;

public interface ISubtaskService
{
    List<SubtaskModel> GetSubtasks(int progressId);
    void AddSubtask(SubtaskModel subtask);
    void DeleteSubtask(int subtaskId);
    void UpdateSubtask(SubtaskModel subtask);
    void DeleteProgressSubtask(int progressId);
}