using System.ComponentModel.DataAnnotations;

namespace MVC_CodingTracker.Models
{
    public class DevelopmentTime : IValidatableObject
    {

        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateEnd { get; set; }
        public string? Comments { get; set; }

        public TimeSpan? Duration { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateEnd <= DateStart)
            {
                yield return new ValidationResult(
                    "End date must be greater than start date.",
                    new[] { nameof(DateEnd) }
                );
            }
        }
    }
}
