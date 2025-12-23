using FluentValidation;
using FitnessTracker.Application.DTOs.FoodLog;

namespace FitnessTracker.Application.Validators.FoodLog;

public class CreateFoodLogDtoValidator : AbstractValidator<CreateFoodLogDto>
{
    public CreateFoodLogDtoValidator()
    {
        Include(new FoodLogValidator());
    }
}

