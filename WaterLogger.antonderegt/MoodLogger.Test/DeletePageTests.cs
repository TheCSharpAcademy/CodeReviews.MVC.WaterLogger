using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using MoodLogger.Models;
using MoodLogger.Pages;
using MoodLogger.Services;

namespace MoodLogger.Test;

[Collection("Sequential")]
public class DeletePageTests : IDisposable
{
    private readonly MoodDataService _moodDataService;
    private readonly DeleteModel _deleteModel;

    public DeletePageTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", "DataSource=file::memory:" }
            }.ToDictionary(x => x.Key, x => (string?)x.Value))
            .Build();

        _moodDataService = new MoodDataService(configuration);
        _moodDataService.Initialize();

        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var pageContext = new PageContext(actionContext)
        {
            ViewData = new ViewDataDictionary(modelMetadataProvider, modelState)
        };

        _deleteModel = new DeleteModel(_moodDataService)
        {
            PageContext = pageContext
        };
    }

    [Fact]
    public void DeletePageOnGet_WithSeededDatabase_ShouldGetRecord()
    {
        // Arrange
        _moodDataService.AddMoodRecord(new() { Date = DateTime.Now, MoodLevel = 5 });
        int expected = 5;

        // Act
        _deleteModel.OnGet(1);

        // Assert
        Assert.NotNull(_deleteModel.MoodRecord);
        Assert.Equal(expected, _deleteModel.MoodRecord.MoodLevel);
    }

    [Fact]
    public void DeletePageOnGet_WithEmptyDatabase_ShouldHaveDefaultMoodRecord()
    {
        // Arrange
        Mood expected = new();

        // Act
        _deleteModel.OnGet(1);

        // Assert
        Assert.Equivalent(expected, _deleteModel.MoodRecord);
    }

    [Fact]
    public void DeletePageOnPost_WithSeededDatabase_ShouldRemoveRecord()
    {
        // Arrange
        _moodDataService.AddMoodRecord(new() { Date = DateTime.Now, MoodLevel = 5 });
        List<Mood> expected = [];

        // Act
        _deleteModel.OnPost(1);
        var actual = _moodDataService.GetAllRecords();

        // Assert
        Assert.Equivalent(expected, actual);
    }

    public void Dispose()
    {
        SqliteConnection connection = new("DataSource=file::memory:");
        connection.Close();
        connection.Dispose();
        connection.Open();
    }
}
