using System.ComponentModel.DataAnnotations;

namespace RunningLogger.Models
{
    public class Log
    {
        public int LogId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Value for {0} must be positive")]
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; } = "";

        public decimal QuantityInKilometers
        {
            get
            {
                return Unit.StandardiseRunningUnitsToKilometers(this.Quantity, this.UnitName);
            }
        }
    }
}
