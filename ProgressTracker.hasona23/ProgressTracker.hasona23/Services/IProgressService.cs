using ProgressTracker.hasona23.Models;

namespace ProgressTracker.hasona23.Services;

public interface IProgressService
{
    List<ProgressModel> GetProgresses();
    void DeleteProgresses(int progressId);
    void AddProgress(ProgressModel progress);
    void UpdateProgress(ProgressModel progress);
    ProgressModel GetProgress(int progressId);
}