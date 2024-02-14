using System.ComponentModel.DataAnnotations;

namespace ReadToKidsTracker.Validations;

public class ValidateEndPageAttribute : ValidationAttribute
{
    private readonly string _startPagePropertyName;

    public ValidateEndPageAttribute(string startPagePropertyName)
    {
        _startPagePropertyName = startPagePropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startPageProperty = validationContext.ObjectType.GetProperty(_startPagePropertyName);

        if (startPageProperty == null)
        {
            throw new ArgumentException($"Property {_startPagePropertyName} not found on {validationContext.ObjectType.Name}");
        }

        var startPageValue = (int)startPageProperty.GetValue(validationContext.ObjectInstance, null);
        var endPageValue = (int)value;

        if (endPageValue > startPageValue)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult($"End Page must be greater than Start Page.");
        }
    }
}


