using MoodLogger.Models;

namespace MoodLogger.Services;

public interface IMoodDataService
{
    void Initialize();
    void AddMoodRecord(Mood moodRecord);
    List<Mood> GetAllRecords();
    Mood GetById(int id);
    void Update(Mood moodRecord);
    void Delete(int id);
}