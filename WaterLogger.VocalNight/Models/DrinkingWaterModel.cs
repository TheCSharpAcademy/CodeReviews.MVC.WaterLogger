using System.ComponentModel.DataAnnotations;

namespace WaterDrinkingLogger.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public double Quantity { get; set; }
    }
}
