using WeightLogger.samggannon.Data;

namespace WeightLogger.samggannon.Models
{
    public class Weight
    {
        public int Id { get; set; }
        public decimal weightValue { get; set; }
        public DateTime loggedDate { get; set; }

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
