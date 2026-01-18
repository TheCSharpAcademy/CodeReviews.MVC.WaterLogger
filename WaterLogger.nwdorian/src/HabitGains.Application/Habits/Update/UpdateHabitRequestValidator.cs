using FluentValidation;

namespace HabitGains.Application.Habits.Update;

public class UpdateHabitRequestValidator : AbstractValidator<UpdateHabitRequest>
{
    public UpdateHabitRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(3);
        RuleFor(x => x.Measurement).NotEmpty().MaximumLength(50);
    }
}
