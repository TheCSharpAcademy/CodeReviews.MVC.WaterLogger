using System.ComponentModel.DataAnnotations;

namespace TCSA_WaterLogger.Arashi256.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime Date { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public float Quantity { get; set; }
        public string Unit { get; set; } = "Glass";
    }
}