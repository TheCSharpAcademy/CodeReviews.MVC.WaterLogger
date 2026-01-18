using FluentValidation;

namespace HabitGains.Application.Entries.GetByHabitId;

public class GetEntriesByHabitIdRequestValidator : AbstractValidator<GetEntriesByHabitIdRequest>
{
    private static readonly string[] _allowedSortColumns = { "quantity", "date", "created" };

    public GetEntriesByHabitIdRequestValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than zero.").LessThan(1000);
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(3)
            .WithMessage("Page size must be greater than or equal to 3.")
            .LessThanOrEqualTo(20)
            .WithMessage("Page size must be less than or equal to 20.");
        RuleFor(x => x.SortColumn).Must(c => _allowedSortColumns.Contains(c)).WithMessage("Invalid sort column.");
        RuleFor(x => x.SortOrder).Must(o => o is "asc" or "desc").WithMessage("Sort order must be 'asc' or 'desc'.");
        RuleFor(x => x.QuantityFrom).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.QuantityTo).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
