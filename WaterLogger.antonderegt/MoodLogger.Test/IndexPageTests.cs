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
public class IndexPageTests : IDisposable
{
    private readonly MoodDataService _moodDataService;
    private readonly IndexModel _indexModel;

    public IndexPageTests()
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

        _indexModel = new IndexModel(_moodDataService)
        {
            PageContext = pageContext
        };
    }

    [Fact]
    public void IndexPageOnGet_WithSeededDatabase_ShouldReturnListOfRecords()
    {
        // Arrange
        _moodDataService.AddMoodRecord(new Mood { Date = DateTime.Now, MoodLevel = 5 });

        // Act
        _indexModel.OnGet();

        // Assert
        Assert.NotNull(_indexModel.Records);
        Assert.Single(_indexModel.Records);
    }

    [Fact]
    public void IndexPageOnGet_WithSeededDatabase_ShouldPopulateViewDataAverage()
    {
        // Arrange
        _moodDataService.AddMoodRecord(new Mood { Date = DateTime.Now, MoodLevel = 5 });
        string expected = "5.00";

        // Act
        _indexModel.OnGet();

        // Assert
        Assert.NotNull(_indexModel.ViewData["Average"]);
        Assert.Equivalent(expected, _indexModel.ViewData["Average"]);
    }

    [Fact]
    public void IndexPageOnGet_WithEmptyDatabase_ShouldReturnNull()
    {
        // Arrange
        List<Mood> expected = [];

        // Act
        _indexModel.OnGet();

        // Assert
        Assert.Equivalent(expected, _indexModel.Records);
    }

    [Fact]
    public void IndexPageOnGet_WithEmptyDatabase_ShouldPopulateViewDataAverageWith0()
    {
        // Arrange
        string expected = "0";

        // Act
        _indexModel.OnGet();

        // Assert
        Assert.NotNull(_indexModel.ViewData["Average"]);
        Assert.Equivalent(expected, _indexModel.ViewData["Average"]);
    }

    public void Dispose()
    {
        SqliteConnection connection = new("DataSource=file::memory:");
        connection.Close();
        connection.Dispose();
        connection.Open();
    }
}
