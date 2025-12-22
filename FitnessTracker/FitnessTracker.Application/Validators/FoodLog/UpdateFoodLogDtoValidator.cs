using FitnessTracker.Application.DTOs.FoodLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.FoodLog;

public class UpdateFoodLogDtoValidator : AbstractValidator<CreateFoodLogDto>
{
    public UpdateFoodLogDtoValidator()
    {
        Include(new CreateFoodLogDtoValidator());
    }
}
