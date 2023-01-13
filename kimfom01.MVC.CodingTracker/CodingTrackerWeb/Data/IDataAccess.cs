using CodingTrackerWeb.Models;

namespace CodingTrackerWeb.Data;

public interface IDataAccess
{
    public void InsertRecord(CodingHour codingHour);
    public void DeleteRecord(int id);
    public List<CodingHour> GetAllRecords();
    public void UpdateRecord(int id, CodingHour codingHour);
    public CodingHour GetById(int id);
}