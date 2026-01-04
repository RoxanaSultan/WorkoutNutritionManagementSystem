using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IWorkoutService
{
    Task<Result<IEnumerable<WorkoutDto>>> GetAllAsync();
    Task<Result<WorkoutDto>> GetByIdAsync(int id);
    Task<Result<WorkoutDto>> CreateAsync(CreateWorkoutDto createWorkoutDto);
    Task<Result<WorkoutDto>> UpdateAsync(int id, UpdateWorkoutDto updateWorkoutDto);
    Task<Result<WorkoutDto>> DeleteAsync(int id);

    Task<Result<IEnumerable<WorkoutExerciseDto>>> GetExercisesByWorkout(int workoutId);
    Task<Result> AddExerciseAsync(int workoutId, CreateWorkoutExerciseDto dto);
    Task<Result> UpdateExerciseAsync(int workoutId, int exerciseId, UpdateWorkoutExerciseDto dto);
    Task<Result> RemoveExerciseAsync(int workoutId, int exerciseId);
}
