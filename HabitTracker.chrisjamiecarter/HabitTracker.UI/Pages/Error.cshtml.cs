using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitTracker.WebUI.Pages;

/// <summary>
/// Page that displays an error message.
/// </summary>
public class ErrorModel : PageModel
{
    #region Fields

    public string? Message { get; set; }

    public bool ShowErrorMessage => !string.IsNullOrEmpty(Message);

    #endregion
    #region Methods

    public void OnGet(string errorMessage)
    {
        Message = errorMessage;
    }

    #endregion
}
