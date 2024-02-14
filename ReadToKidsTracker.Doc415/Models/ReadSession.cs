using ReadToKidsTracker.Validations;
using System.ComponentModel.DataAnnotations;

namespace ReadToKidsTracker.Models;

public class ReadSession
{

    public int Id { get; set; }

    [Required(ErrorMessage = "Book name is required.")]
    public string BookName { get; set; }

    [Required(ErrorMessage = "Start page is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be a positive numer")]
    public int StartPage { get; set; }

    [Required(ErrorMessage = "End page is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be a positive numer")]
    [ValidateEndPage("StartPage")]
    public int EndPage { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Please enter the date of reading session.")]
    [DateValidation(ErrorMessage = "Date of read session can not be in the future.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Read session duration is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive numer")]
    public int Duration { get; set; }

    public string? Comments { get; set; }

    
    public int TotalPages { get; set; }

}
