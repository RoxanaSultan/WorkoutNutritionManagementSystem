using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services.Validators;

public interface IWorkoutExerciseServiceValidator
{
    Task<Result> ValidateCreateAsync(CreateWorkoutExerciseDto dto);
}