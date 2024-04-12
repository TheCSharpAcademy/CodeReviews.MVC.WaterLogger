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
public class UpdatePageTests : IDisposable
{
    private readonly IMoodDataService _moodDataService;
    private readonly UpdateModel _updateModel;

    public UpdatePageTests()
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

        _updateModel = new UpdateModel(_moodDataService)
        {
            PageContext = pageContext
        };
    }

    [Fact]
    public void UpdatePageOnGet_WithSeededDatabase_ShouldGetRecord()
    {
        // Arrange
        _moodDataService.AddMoodRecord(new() { Date = DateTime.Now, MoodLevel = 5 });
        int expected = 5;

        // Act
        _updateModel.OnGet(1);

        // Assert
        Assert.NotNull(_updateModel.MoodRecord);
        Assert.Equal(expected, _updateModel.MoodRecord.MoodLevel);
    }

    [Fact]
    public void UpdatePageOnGet_WithEmptyDatabase_ShouldHaveDefaultMoodRecord()
    {
        // Arrange
        Mood expected = new();

        // Act
        _updateModel.OnGet(1);

        // Assert
        Assert.Equivalent(expected, _updateModel.MoodRecord);
    }

    [Fact]
    public void UpdatePageOnPost_WithValidModel_ShouldAddRecord()
    {
        // Arrange
        Mood mood = new() { Date = DateTime.Now, MoodLevel = 5, Id = 1 };
        _moodDataService.AddMoodRecord(mood);
        int expected = 3;
        mood.MoodLevel = expected;
        _updateModel.MoodRecord = mood;

        // Act
        _updateModel.OnPost();
        var actual = _moodDataService.GetAllRecords()[0].MoodLevel;

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void UpdatePageOnPost_WithInValidModel_ShouldNotAddRecord()
    {
        // Arrange
        int invalidMoodLevel = 15;
        Mood mood = new() { Date = DateTime.Now, MoodLevel = invalidMoodLevel, Id = 1 };
        _updateModel.MoodRecord = mood;
        List<Mood> expected = [];

        // Act
        _updateModel.OnPost();
        List<Mood> actual = _moodDataService.GetAllRecords();

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
