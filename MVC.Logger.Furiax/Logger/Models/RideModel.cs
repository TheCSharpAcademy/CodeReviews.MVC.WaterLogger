using System.ComponentModel.DataAnnotations;

namespace Logger.Models
{
    public class RideModel
    {
        public  int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "Value for {0} can't be negative.")]
        public double Distance { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
