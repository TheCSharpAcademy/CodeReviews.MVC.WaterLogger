namespace MVC.WaterLogger.K_MYR.Models
{
    public class RecordModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Quantity { get; set; }
        public int HabitId { get; set; }
    }
}
