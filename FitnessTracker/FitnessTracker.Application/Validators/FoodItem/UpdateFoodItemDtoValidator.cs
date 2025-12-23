using FitnessTracker.Application.DTOs.FoodItem;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodItem;

public class UpdateFoodItemDtoValidator : AbstractValidator<CreateFoodItemDto>
{
    public UpdateFoodItemDtoValidator()
    {
        Include(new FoodItemValidator());
    }
}
