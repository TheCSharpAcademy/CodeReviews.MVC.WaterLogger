using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CalorieLogger.Models;

public class CalorieIntakeModel
{
    [Key]
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "You must enter a food or drink.")]
    [DisplayName("Food or Drink")]
    public string Food { get; set; } = string.Empty;

    [Range(1, 2000, ErrorMessage = "Value for {0} must be positive.")]
    public int Calories { get; set; }
}
