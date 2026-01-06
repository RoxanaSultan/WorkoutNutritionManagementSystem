using FitnessTracker.Application.Common;
using FitnessTracker.Application.Constants;
using FitnessTracker.Application.DTOs.Exercise;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Exercise;

public class ExerciseValidator : AbstractValidator<ExerciseBase>
{
    public ExerciseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ErrorMessages.ExerciseNameRequired)
            .MaximumLength(100).WithMessage(ErrorMessages.ExerciseNameMaxLength100);

        RuleFor(x => x.MuscleGroup)
            .Must(BeValidMuscleGroup)
            .WithMessage(ErrorMessages.ExerciseMuscleGroupInvalid)
            .When(x => !string.IsNullOrWhiteSpace(x.MuscleGroup));

        RuleFor(x => x.DifficultyLevel)
            .Must(BeValidDifficulty)
            .WithMessage(ErrorMessages.ExerciseDifficultyLevelInvalid)
            .When(x => !string.IsNullOrWhiteSpace(x.DifficultyLevel));
    }

    private static bool BeValidMuscleGroup(string? value)
        => value != null && ExerciseOptions.MuscleGroups.Contains(value.Trim());

    private static bool BeValidDifficulty(string? value)
        => value != null && ExerciseOptions.DifficultyLevels.Contains(value.Trim());
}