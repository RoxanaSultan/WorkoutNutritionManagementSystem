using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IWorkoutRepository : IGenericRepository<Workout>
{
    Task<bool> ExistsSameStartTimeAsync(int userId, DateTime dateToMinute, int? ignoreWorkoutId = null);

    Task<bool> WorkoutExerciseExistsAsync(int workoutId, int exerciseId);
    Task<IEnumerable<WorkoutExercise>> GetWorkoutExercisesByWorkoutIdAsync(int workoutId);
    Task AddWorkoutExerciseAsync(WorkoutExercise workoutExercise);
    Task<WorkoutExercise?> GetWorkoutExerciseAsync(int workoutId, int exerciseId);
    Task UpdateWorkoutExerciseAsync(WorkoutExercise entity);
    Task RemoveWorkoutExerciseAsync(WorkoutExercise workoutExercise);
    Task<List<int>> GetWorkoutIdsByUserIdAsync(int userId);
    Task DeleteByUserIdAsync(int userId);
    Task DeleteByWorkoutIdsAsync(IEnumerable<int> workoutIds);
}
