using FitnessTracker.Application.DTOs.Exercise;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Exercise;

public class UpdateExerciseDtoValidator : AbstractValidator<UpdateExerciseDto>
{
    public UpdateExerciseDtoValidator()
    {
        Include(new CreateExerciseDtoValidator());
    }
}
