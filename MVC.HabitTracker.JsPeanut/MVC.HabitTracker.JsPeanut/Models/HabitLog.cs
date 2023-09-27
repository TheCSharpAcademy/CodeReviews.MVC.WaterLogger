using System.ComponentModel.DataAnnotations;

namespace MVC.HabitTracker.JsPeanut.Models
{
    public class HabitLog
    {
        public int Id { get; set; }

        public string HabitTypeName { get; set; }

        //public HabitType HabitType { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public TimeSpan? Time { get; set; }

        public int Quantity { get; set; }
    }
}
