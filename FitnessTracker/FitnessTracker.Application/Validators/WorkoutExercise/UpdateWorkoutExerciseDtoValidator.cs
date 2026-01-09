using FitnessTracker.Application.DTOs.WorkoutExercise;
using FluentValidation;

namespace FitnessTracker.Application.Validators.WorkoutExercise;

public class UpdateWorkoutExerciseDtoValidator : AbstractValidator<UpdateWorkoutExerciseDto>
{
    public UpdateWorkoutExerciseDtoValidator()
    {
        Include(new WorkoutExerciseDtoValidator());
    }
}
