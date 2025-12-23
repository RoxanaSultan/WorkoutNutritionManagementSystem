using FitnessTracker.Application.DTOs.Workout;
using FluentValidation;
namespace FitnessTracker.Application.Validators.Workout;

public class UpdateWorkoutDtoValidator : AbstractValidator<UpdateWorkoutDto>
{
    public UpdateWorkoutDtoValidator()
    {
        Include(new WorkoutValidator());
    }
}
