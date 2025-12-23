using FluentValidation;
using FitnessTracker.Application.Constants;
using FitnessTracker.Application.DTOs.Exercise;

namespace FitnessTracker.Application.Validators.Exercise;

public class CreateExerciseDtoValidator : AbstractValidator<CreateExerciseDto>
{
    public CreateExerciseDtoValidator()
    {
        Include(new ExerciseValidator());
    }
}