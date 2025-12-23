using FitnessTracker.Application.DTOs.FoodLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodLog;

public class FoodLogValidator : AbstractValidator<FoodLogBase>
{
    public FoodLogValidator()
    {
        RuleFor(x => x.Servings)
            .GreaterThan(0).WithMessage("Servings must be greater than 0.");

        RuleFor(x => x.LogDate)
            .Must(d => d.ToUniversalTime() <= DateTime.UtcNow).WithMessage("Log date can't be in the future.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}
