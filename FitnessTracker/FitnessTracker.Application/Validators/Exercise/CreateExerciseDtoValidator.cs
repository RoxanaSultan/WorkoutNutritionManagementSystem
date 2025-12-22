using FluentValidation;
using FitnessTracker.Application.Constants;
using FitnessTracker.Application.DTOs.Exercise;

namespace FitnessTracker.Application.Validators.Exercise;

public class CreateExerciseDtoValidator : AbstractValidator<CreateExerciseDto>
{
    public CreateExerciseDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.MuscleGroup)
            .Must(BeValidMuscleGroup)
            .WithMessage("MuscleGroup must be one of: back, chest, arms, shoulders, legs, abdomen.")
            .When(x => !string.IsNullOrWhiteSpace(x.MuscleGroup));

        RuleFor(x => x.DifficultyLevel)
            .Must(BeValidDifficulty)
            .WithMessage("DifficultyLevel must be one of: beginner, intermediate, advanced.")
            .When(x => !string.IsNullOrWhiteSpace(x.DifficultyLevel));
    }

    private static bool BeValidMuscleGroup(string? value)
        => value != null && ExerciseOptions.MuscleGroups.Contains(value.Trim());

    private static bool BeValidDifficulty(string? value)
        => value != null && ExerciseOptions.DifficultyLevels.Contains(value.Trim());
}