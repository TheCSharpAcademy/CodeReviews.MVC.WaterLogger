namespace MVC.WaterLogger.K_MYR.Models
{
    public class HabitModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Measurement { get; set; }
        public string Icon { get; set; }
        public ICollection<RecordModel> Records { get; set; } = new List<RecordModel>();
    }
}
