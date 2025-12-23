using FitnessTracker.Application.DTOs.FoodItem;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodItem;

public class CreateFoodItemDtoValidator : AbstractValidator<CreateFoodItemDto>
{
    public CreateFoodItemDtoValidator()
    {
        Include(new FoodItemValidator());
    }
}
