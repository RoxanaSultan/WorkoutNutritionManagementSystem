using FitnessTracker.Application.DTOs.Goal;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Goal;

public class UpdateGoalDtoValidator : AbstractValidator<UpdateGoalDto>
{
    public UpdateGoalDtoValidator()
    {
        Include(new GoalDtoValidator());
    }
}