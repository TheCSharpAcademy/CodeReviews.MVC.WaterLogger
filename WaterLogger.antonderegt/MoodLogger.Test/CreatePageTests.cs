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
public class CreatePageTests : IDisposable
{
    private readonly MoodDataService _moodDataService;
    private readonly CreateModel _createModel;

    public CreatePageTests()
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

        _createModel = new CreateModel(_moodDataService)
        {
            PageContext = pageContext
        };
    }

    [Fact]
    public void CreatePageOnPost_WithValidModel_ShouldAddRecord()
    {
        // Arrange
        Mood mood = new() { Date = DateTime.Now, MoodLevel = 5, Id = 1 };
        _createModel.MoodRecord = mood;
        int expected = 5;

        // Act
        _createModel.OnPost();
        var actual = _moodDataService.GetAllRecords()[0].MoodLevel;

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void CreatePageOnPost_WithInValidModel_ShouldNotAddRecord()
    {
        // Arrange
        int invalidMoodLevel = 15;
        Mood mood = new() { Date = DateTime.Now, MoodLevel = invalidMoodLevel, Id = 1 };
        _createModel.MoodRecord = mood;
        List<Mood> expected = [];

        // Act
        _createModel.OnPost();
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
