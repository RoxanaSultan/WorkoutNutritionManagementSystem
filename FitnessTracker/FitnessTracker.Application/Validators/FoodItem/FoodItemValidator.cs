using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.FoodItem;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodItem;

public class FoodItemValidator : AbstractValidator<FoodItemBase>
{
    public FoodItemValidator()
    {
        RuleFor(x => x.Calories)
            .GreaterThan(0).WithMessage(ErrorMessages.FoodItemCaloriesPositive);

        RuleFor(x => x.Protein)
            .GreaterThan(0).WithMessage(ErrorMessages.FoodItemProteinPositive);

        RuleFor(x => x.Carbs)
            .GreaterThan(0).WithMessage(ErrorMessages.FoodItemCarbsPositive);

        RuleFor(x => x.Fat)
            .GreaterThan(0).WithMessage(ErrorMessages.FoodItemFatPositive);
    }
}
