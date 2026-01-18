using System.ComponentModel.DataAnnotations;

namespace HabitGains.Web.ViewModels.Entries;

public class CreateEntryForm
{
    [Required(ErrorMessage = "Please select a date.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Please enter a quantity.")]
    public decimal Quantity { get; set; }
}
