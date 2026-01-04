using FitnessTracker.Application.DTOs.Workout;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Workout;

public class CreateWorkoutDtoValidator : AbstractValidator<CreateWorkoutDto>
{
    public CreateWorkoutDtoValidator()
    {
        Include(new WorkoutValidator());
    }
}