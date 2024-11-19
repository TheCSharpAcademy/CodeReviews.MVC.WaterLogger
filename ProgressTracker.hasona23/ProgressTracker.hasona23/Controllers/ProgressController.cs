using ProgressTracker.hasona23.Models;
using ProgressTracker.hasona23.Services;

namespace ProgressTracker.hasona23.Controllers;

public class ProgressController
{
    private readonly IProgressService _progressService;
    private readonly ISubtaskService _subtaskService;

    public ProgressController(IProgressService progressService, ISubtaskService subtaskService)
    {
        _progressService = progressService;
        _subtaskService = subtaskService;
    }
    public void AddProgress(ProgressModel progressModel)
    {
        _progressService.AddProgress(progressModel);
        int id = _progressService.GetProgresses()[_progressService.GetProgresses().Count-1].Id;
        foreach (var subtask in progressModel.SubTasks)
        {
            subtask.ProgressId = id;
            _subtaskService.AddSubtask(subtask);
        }
    }

    public ProgressModel GetById(int id)
    {
        var progressModel = _progressService.GetProgress(id);
        progressModel.SubTasks = _subtaskService.GetSubtasks(id);
        return progressModel;
    }
    public void DeleteProgress(int progressId)
    {
        _subtaskService.DeleteProgressSubtask(progressId);
        _progressService.DeleteProgresses(progressId);
    }

    public void UpdateProgress(ProgressModel progressModel)
    {
        _progressService.UpdateProgress(progressModel);
        foreach (var subtask in progressModel.SubTasks)
        {
            _subtaskService.UpdateSubtask(subtask);
        }
    }

    public List<ProgressModel> GetAllProgress()
    {
        var progresses = _progressService.GetProgresses();
        foreach (var progressModel in progresses)
        {
            progressModel.SubTasks = _subtaskService.GetSubtasks(progressModel.Id);
        }

        return progresses;
    }
}