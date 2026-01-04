using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Infrastructure.Data;
using FitnessTracker.Infrastructure.Repositories;

namespace FitnessTracker.Infrastructure;

public sealed class UnitOfWork(
    FitnessTrackerDbContext context
        ) : IUnitOfWork
{
    private readonly FitnessTrackerDbContext _context = context;

    private ExerciseRepository? _exercises;
    private FoodItemRepository? _foodItems;
    private FoodLogRepository? _foodLogs;
    private GoalRepository? _goals;
    private MeasurementLogRepository? _measurementLogs;
    private UserRepository? _users;
    private WorkoutRepository? _workouts;

    public IExerciseRepository Exercises => _exercises ??= new ExerciseRepository(_context);
    public IFoodItemRepository FoodItems => _foodItems ??= new FoodItemRepository(_context);
    public IFoodLogRepository FoodLogs => _foodLogs ??= new FoodLogRepository(_context);
    public IGoalRepository Goals => _goals ??= new GoalRepository(_context);
    public IMeasurementLogRepository MeasurementLogs => _measurementLogs ??= new MeasurementLogRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IWorkoutRepository Workouts => _workouts ??= new WorkoutRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
