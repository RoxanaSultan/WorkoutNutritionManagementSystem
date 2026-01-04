using FitnessTracker.Application.DTOs.Exercise;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Exercise;

public class CreateExerciseDtoValidator : AbstractValidator<CreateExerciseDto>
{
    public CreateExerciseDtoValidator()
    {
        Include(new ExerciseValidator());
    }
}