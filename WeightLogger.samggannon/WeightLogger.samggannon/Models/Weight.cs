using System.ComponentModel.DataAnnotations;
using WeightLogger.samggannon.Data;

namespace WeightLogger.samggannon.Models
{
    public class Weight
    {
        public int Id { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public decimal weightValue { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yy}", ApplyFormatInEditMode = true)]
        public DateTime? loggedDate { get; set; }

        public static Weight MapFromDto(WeightRecordDto dto)
        {
            if (dto == null) 
                throw new ArgumentNullException(nameof(dto));

            return new Weight
            {
                Id = dto.Id,
                weightValue = dto.weight,
                loggedDate = dto.loggedDate
            };
        }
    }
}
