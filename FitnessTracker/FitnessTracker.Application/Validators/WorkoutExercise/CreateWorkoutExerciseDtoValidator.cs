using FluentValidation;
using FitnessTracker.Application.DTOs.WorkoutExercise;

namespace FitnessTracker.Application.Validators.WorkoutExercise;

public class CreateWorkoutExerciseDtoValidator : AbstractValidator<CreateWorkoutExerciseDto>
{
    public CreateWorkoutExerciseDtoValidator()
    {
        Include(new WorkoutExerciseDtoValidator());
    }
}