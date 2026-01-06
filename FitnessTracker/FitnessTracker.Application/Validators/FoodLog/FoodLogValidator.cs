using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.FoodLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodLog;

public class FoodLogValidator : AbstractValidator<FoodLogBase>
{
    public FoodLogValidator()
    {
        RuleFor(x => x.Servings)
            .GreaterThan(0).WithMessage(ErrorMessages.FoodLogServingsPositive);

        RuleFor(x => x.LogDate)
            .Must(d => d.ToUniversalTime() <= DateTime.UtcNow)
            .WithMessage(ErrorMessages.FoodLogDateNotInFuture);

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage(ErrorMessages.FoodLogQuantityPositive);
    }
}
