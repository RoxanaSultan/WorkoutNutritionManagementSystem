namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IExerciseRepository Exercises { get; }
    IFoodItemRepository FoodItems { get; }
    IFoodLogRepository FoodLogs { get; }
    IGoalRepository Goals { get; }
    IMeasurementLogRepository MeasurementLogs { get; }
    IUserRepository Users { get; }
    IWorkoutRepository Workouts { get; }
    IWorkoutExerciseRepository WorkoutExercises { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
