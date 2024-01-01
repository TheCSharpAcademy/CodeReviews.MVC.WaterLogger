using System.ComponentModel.DataAnnotations;

namespace MVC.WaterLogger.K_MYR.Models
{
    public class RecordModel
    {
        public int Id { get; set; }  
        [Required]      
        public DateTime Date { get; set; }
        [Required]  
        [Range(0, float.MaxValue, ErrorMessage = $"Please enter a value between 0 and ~3.4E+38!")]
        public float Quantity { get; set; } 
        [Required]      
        public int HabitId { get; set; }
    }
}
