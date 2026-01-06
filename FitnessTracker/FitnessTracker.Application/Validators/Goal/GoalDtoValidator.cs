using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.Goal;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Goal;

public class GoalDtoValidator : AbstractValidator<GoalBase>
{
    public GoalDtoValidator()
    {
        RuleFor(x => x.TargetValue)
            .GreaterThan(0).WithMessage(ErrorMessages.GoalTargetValuePositive);

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage(ErrorMessages.GoalStartDateBeforeOrEqualEndDate);
    }
}
