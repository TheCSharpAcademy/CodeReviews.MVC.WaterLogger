using FluentValidation;

namespace HabitGains.Application.Habits.Create;

public class CreateHabitRequestValidator : AbstractValidator<CreateHabitRequest>
{
    public CreateHabitRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(3);
        RuleFor(x => x.Measurement).NotEmpty().MaximumLength(50);
    }
}
