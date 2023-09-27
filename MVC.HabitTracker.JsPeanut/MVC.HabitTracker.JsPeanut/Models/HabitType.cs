using System.ComponentModel.DataAnnotations;

namespace MVC.HabitTracker.JsPeanut.Models
{
    public class HabitType
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public string Name { get; set; }

        public string Measurability { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; }
    }
}
