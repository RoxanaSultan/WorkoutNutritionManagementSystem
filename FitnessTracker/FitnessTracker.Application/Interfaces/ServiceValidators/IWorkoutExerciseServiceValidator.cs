using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IWorkoutExerciseServiceValidator
{
    Task<Result> ValidateCreateAsync(CreateWorkoutExerciseDto dto);
}