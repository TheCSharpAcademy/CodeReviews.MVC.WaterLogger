using CodingTrackerWeb.Context;
using Microsoft.EntityFrameworkCore;

namespace CodingTrackerWeb.Helper;

public class DataHelper
{
    public static async Task ManageDataAsync(IServiceProvider svcProvider)
    {
        //Service: An instance of db context
        var dbContextSvc = svcProvider.GetRequiredService<DatabaseContext>();

        //Migration: This is the programmatic equivalent to Update-Database
        await dbContextSvc.Database.GetPendingMigrationsAsync();
    }
}