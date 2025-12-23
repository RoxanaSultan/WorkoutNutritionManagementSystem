using FitnessTracker.Application.DTOs.Goal;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Goal;

public class CreateGoalDtoValidator : AbstractValidator<CreateGoalDto>
{
    public CreateGoalDtoValidator()
    {
        Include(new GoalDtoValidator());

        RuleFor(x => x.UserId)
           .GreaterThan(0).WithMessage("UserId must be greater than 0.");
    }
}
