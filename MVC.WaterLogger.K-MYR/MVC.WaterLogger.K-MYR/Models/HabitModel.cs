using System.ComponentModel.DataAnnotations;

namespace MVC.WaterLogger.K_MYR.Models
{
    public class HabitModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength =1, ErrorMessage = "Please enter a valid name (0-250 characters)!")]               
        public string? Name { get; set; }
        [Required]
        [StringLength(250, MinimumLength =1, ErrorMessage = "Please enter a valid name (0-250 characters)!")]  
        public string? Measurement { get; set; }
        [Required]        
        public string? Icon { get; set; }
        public ICollection<RecordModel> Records { get; set; } = new List<RecordModel>();
    }
}
