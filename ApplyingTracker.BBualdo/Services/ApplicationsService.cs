using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class ApplicationsService(AppDbContext dbContext) : IApplicationsService
{
    private readonly AppDbContext _dbContext = dbContext;
    
    public async Task<IEnumerable<Application>> GetApplications()
    {
        return await _dbContext.Applications.OrderByDescending(a => a.Date).ToListAsync();
    }

    public async Task<Application?> GetApplicationById(int id)
    {
        return await _dbContext.Applications.FindAsync(id);
    }

    public async Task AddApplication(Application application)
    {
        await _dbContext.Applications.AddAsync(application);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateApplication(Application application)
    {
        _dbContext.Entry(application).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteApplication(Application application)
    {
        _dbContext.Remove(application);
        await _dbContext.SaveChangesAsync();
    }
}