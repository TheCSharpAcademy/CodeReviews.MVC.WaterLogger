using Data.Models;

namespace Services;

public interface IApplicationsService
{
    Task<IEnumerable<Application>> GetApplications();
    Task<Application?> GetApplicationById(int id);
    Task AddApplication(Application application);
    Task UpdateApplication(Application application);
    Task DeleteApplication(Application application);
}