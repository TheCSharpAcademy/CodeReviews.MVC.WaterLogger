using System.ComponentModel.DataAnnotations;

namespace MVC.WaterLogger.K_MYR.Models
{
    public class RecordModel
    {
        public int Id { get; set; }              
        public DateTime Date { get; set; }
        [Required]  
        [Range(0, float.MaxValue, ErrorMessage = "Invalid Input!")]
        public float Quantity { get; set; }        
        public int HabitId { get; set; }
    }
}
