namespace FitnessTracker.Application.Interfaces;

public interface IUnitOfWork
{
    IExerciseRepository Exercises { get; }
    IFoodItemRepository FoodItems { get; }
    IFoodLogRepository FoodLogs { get; }
    IGoalRepository Goals { get; }
    IMeasurementLogRepository MeasurementLogs { get; }
    IUserRepository Users { get; }
    IWorkoutRepository Workouts { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
