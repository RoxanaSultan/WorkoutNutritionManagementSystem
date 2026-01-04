using FitnessTracker.Application.DTOs.FoodLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodLog;

public class CreateFoodLogDtoValidator : AbstractValidator<CreateFoodLogDto>
{
    public CreateFoodLogDtoValidator()
    {
        Include(new FoodLogValidator());
    }
}

