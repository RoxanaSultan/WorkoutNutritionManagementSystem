using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IWorkoutExerciseRepository : IGenericRepository<WorkoutExercise>
{
    Task<WorkoutExercise?> GetByIdAsync(int workoutId, int exerciseId);
    Task DeleteAsync(int workoutId, int exerciseId);
    Task<bool> ExistsAsync(int workoutId, int exerciseId);
    Task DeleteByWorkoutIdsAsync(IEnumerable<int> workoutIds);
}
