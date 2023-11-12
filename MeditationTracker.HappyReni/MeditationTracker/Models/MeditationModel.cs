using System.ComponentModel.DataAnnotations;

namespace MeditationTracker.Models
{
    public class MeditationModel
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int Duration { get; set; }
    }
}
