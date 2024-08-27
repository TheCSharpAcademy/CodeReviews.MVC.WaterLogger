using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class Application
{
    public int Id { get; set; }
    [DisplayName("Company Name")] [Required] public string? CompanyName { get; set; }
    [Required] public DateOnly Date { get; set; }
    [DisplayName("With Cover Letter?")] public bool IsCoverLetterIncluded { get; set; }
    public StatusOptions Status { get; set; }
}