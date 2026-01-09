using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FluentValidation;

namespace FitnessTracker.Application.Validators.WorkoutExercise;

public class WorkoutExerciseDtoValidator : AbstractValidator<WorkoutExerciseBase>
{
    public WorkoutExerciseDtoValidator()
    {
        RuleFor(x => x.Sets)
            .GreaterThan(0).WithMessage(ErrorMessages.WorkoutExerciseSetsPositive)
            .When(x => x.Sets.HasValue);

        RuleFor(x => x.Reps)
            .GreaterThan(0).WithMessage(ErrorMessages.WorkoutExerciseRepsPositive)
            .When(x => x.Reps.HasValue);

        RuleFor(x => x.WeightUsed)
            .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.WorkoutExerciseWeightUsedNonNegative)
            .When(x => x.WeightUsed.HasValue);
    }
}
