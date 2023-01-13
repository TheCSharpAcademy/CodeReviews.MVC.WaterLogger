using System.Globalization;
using CodingTrackerWeb.Context;
using CodingTrackerWeb.Models;

namespace CodingTrackerWeb.Repositories;

public class CodingHourRepository : Repository<CodingHour>, ICodingHourRepository
{
    private readonly DatabaseContext _dbContext;

    public CodingHourRepository(DatabaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void InsertRecord(CodingHour codingHour)
    {
        codingHour.Duration = GetDuration(codingHour.StartTime, codingHour.EndTime);
        base.InsertRecord(codingHour);
        _dbContext.SaveChanges();
    }

    public override void UpdateRecord(int id, CodingHour codingHour)
    {
        codingHour.StartTime = codingHour.StartTime;
        codingHour.EndTime = codingHour.EndTime;
        codingHour.Duration = GetDuration(codingHour.StartTime, codingHour.EndTime);
        base.UpdateRecord(id, codingHour);
        _dbContext.SaveChanges();
    }

    public override void DeleteRecord(int id)
    {
        base.DeleteRecord(id);
        _dbContext.SaveChanges();
    }

    private string GetDuration(string startTime, string endTime)
    {
        DateTime parsedStartTime = DateTime.ParseExact(startTime, "HH:mm", null, DateTimeStyles.None);
        DateTime parsedEndTime = DateTime.ParseExact(endTime, "HH:mm", null, DateTimeStyles.None);

        TimeSpan duration = parsedEndTime.Subtract(parsedStartTime);

        if (duration < TimeSpan.Zero)
        {
            duration += TimeSpan.FromDays(1);
        }

        return duration.ToString();
    }
}