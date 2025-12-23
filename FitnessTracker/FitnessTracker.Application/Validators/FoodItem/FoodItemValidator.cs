using FitnessTracker.Application.DTOs.FoodItem;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodItem;

public class FoodItemValidator : AbstractValidator<FoodItemBase>
{
    public FoodItemValidator()
    {
        RuleFor(x => x.Calories)
            .GreaterThan(0).WithMessage("Calories must be greater than 0.");

        RuleFor(x => x.Protein)
            .GreaterThan(0).WithMessage("Protein must be greater than 0.");

        RuleFor(x => x.Carbs)
            .GreaterThan(0).WithMessage("Carbs must be greater than 0.");

        RuleFor(x => x.Fat)
            .GreaterThan(0).WithMessage("Fat must be greater than 0.");
    }
}
