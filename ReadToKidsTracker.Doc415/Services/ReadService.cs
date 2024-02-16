using ReadToKidsTracker.Models;
using ReadToKidsTracker.TransfromModel;
namespace ReadToKidsTracker.Services;

public class ReadService
{
    private readonly ReadContext _context;

    public ReadService(ReadContext context)
    {
        _context = context;
        SeedDb();
    }

    public List<ReadSessionView> GetReadSessions()
    {
        var sessionsFromDb = _context.ReadSessions.OrderByDescending(x=> x.Date).ToList();
        var sessionsToController = TransformModel.Transform(sessionsFromDb);
        return sessionsToController;
    }

    public ReadSessionView GetSessionById(int id)
    {
        var sessionFromDb = _context.ReadSessions.Single(x => x.Id == id);
        var sessionToView = TransformModel.MaptoView(sessionFromDb);
        return sessionToView;
    }

    public void AddSession(ReadSessionView session)
    {
        var newsession = TransformModel.MaptoDb(session);
        _context.ReadSessions.Add(newsession);
        _context.SaveChanges();
    }

    public void UpdateSession(ReadSessionView updatedSession)
    {
        var currentSession = _context.ReadSessions.FirstOrDefault(x => x.Id == updatedSession.Id);
        currentSession.Date = DateTime.Parse(updatedSession.Date);
        currentSession.StartPage = updatedSession.StartPage;
        currentSession.EndPage = updatedSession.EndPage;
        currentSession.TotalPages = updatedSession.EndPage - updatedSession.StartPage;
        currentSession.Duration = updatedSession.Duration;
        currentSession.Comments = updatedSession.Comments;
        currentSession.BookName = updatedSession.BookName;
        _context.ReadSessions.Update(currentSession);
        _context.Entry(currentSession).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteSession(int id)
    {
        var session = _context.ReadSessions.Single(x => x.Id == id);
        _context.Entry(session).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        _context.Remove(session);
        _context.SaveChanges();
    }

    private void SeedDb()
    {
        int startPage = 1;
        int endPage = 0;
        string name = "mybook";
        DateTime date = DateTime.Now - TimeSpan.FromDays(150);
        Random random = new Random();
        for (int i = 0; i < 50; i++)
        {
            ReadSession newsession = new();
            newsession.BookName = name;
            newsession.StartPage = startPage;
            endPage = startPage + random.Next(1, 20);
            newsession.EndPage = endPage;
            newsession.TotalPages = endPage - startPage;
            newsession.Duration = newsession.TotalPages * 2;
            date += TimeSpan.FromDays(random.Next(1, 3));
            newsession.Date = date;
            newsession.Comments = "great book";
            startPage = endPage;

            _context.Add(newsession);
            _context.SaveChanges();
        }
    }
}
