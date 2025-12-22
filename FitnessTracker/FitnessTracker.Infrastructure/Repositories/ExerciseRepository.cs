using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
{
    private readonly FitnessTrackerDbContext _context;
    public ExerciseRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string normalizedName, int? ignoreExerciseId = null)
    {
        var query = _context.Exercises.AsQueryable();

        if (ignoreExerciseId.HasValue)
            query = query.Where(e => e.Id != ignoreExerciseId.Value);

        return await query.AnyAsync(e =>
            e.Name.ToLower() == normalizedName
        );
    }

    public async Task<bool> IsUsedInPastWorkoutsAsync(int exerciseId, DateTime now)
    {
        return await _context.WorkoutExercises
            .AnyAsync(we =>
                we.ExerciseId == exerciseId &&
                we.Workout.Date < now
            );
    }
}
