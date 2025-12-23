using FitnessTracker.Application.Interfaces;
using FitnessTracker.Infrastructure.Data;

namespace FitnessTracker.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly FitnessTrackerDbContext _context;

    public UnitOfWork(
        FitnessTrackerDbContext context,
        IExerciseRepository exerciseRepository,
        IFoodItemRepository foodItemRepository,
        IFoodLogRepository foodLogRepository,
        IGoalRepository goalRepository,
        IMeasurementLogRepository measurementLogRepository,
        IUserRepository userRepository,
        IWorkoutRepository workoutRepository
        )
    {
        _context = context;
        Exercises = exerciseRepository;
        FoodItems = foodItemRepository;
        FoodLogs = foodLogRepository;
        Goals = goalRepository;
        MeasurementLogs = measurementLogRepository;
        Users = userRepository;
        Workouts = workoutRepository;
    }

    public IExerciseRepository Exercises { get; }
    public IFoodItemRepository FoodItems { get; }
    public IFoodLogRepository FoodLogs { get; }
    public IGoalRepository Goals { get; }
    public IMeasurementLogRepository MeasurementLogs { get; }
    public IUserRepository Users { get; }
    public IWorkoutRepository Workouts { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}
