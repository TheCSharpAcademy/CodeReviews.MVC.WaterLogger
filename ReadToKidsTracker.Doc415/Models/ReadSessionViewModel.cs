namespace ReadToKidsTracker.Models;

public class ReadSessionViewModel
{
    public List<ReadSessionView> ReadSessions { get; set; }
    public int PagesThreshold = 10;
}
