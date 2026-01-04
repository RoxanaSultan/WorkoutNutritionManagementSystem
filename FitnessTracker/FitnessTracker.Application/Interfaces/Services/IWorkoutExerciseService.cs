using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IWorkoutExerciseService
{
    Task<Result<IEnumerable<WorkoutExerciseDto>>> GetAllAsync();
    Task<Result<WorkoutExerciseDto>> GetByIdAsync(int workoutId, int exerciseId);
    Task<Result<WorkoutExerciseDto>> CreateAsync(CreateWorkoutExerciseDto dto);
    Task<Result<WorkoutExerciseDto>> UpdateAsync(int workoutId, int exerciseId, UpdateWorkoutExerciseDto dto);
    Task<Result<WorkoutExerciseDto>> DeleteAsync(int workoutId, int exerciseId);
}
