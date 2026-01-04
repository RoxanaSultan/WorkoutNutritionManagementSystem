using FitnessTracker.Application.DTOs.Goal;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Goal;

public class GoalDtoValidator : AbstractValidator<GoalBase>
{
    public GoalDtoValidator()
    {
        RuleFor(x => x.TargetValue)
            .GreaterThan(0).WithMessage("TargetValue must be a positive number");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Start date must be less than or equal to end date.");
    }
}
