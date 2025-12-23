using FluentValidation;
using FitnessTracker.Application.DTOs.Workout;

namespace FitnessTracker.Application.Validators.Workout;

public class CreateWorkoutDtoValidator : AbstractValidator<CreateWorkoutDto>
{
    public CreateWorkoutDtoValidator()
    {
        Include(new WorkoutValidator());
    }
}