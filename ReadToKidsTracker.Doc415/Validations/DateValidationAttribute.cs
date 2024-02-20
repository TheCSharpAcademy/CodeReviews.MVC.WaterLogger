using System.ComponentModel.DataAnnotations;

namespace ReadToKidsTracker.Validations;

public class DateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var date = value.ToString();
        if (value == null || !(DateTime.Parse(date) is DateTime dateValue))
        {
            return true; // Let the Required attribute handle null values
        }

        return dateValue <= DateTime.Now;
    }
}