using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class WorkoutExerciseRepository : GenericRepository<WorkoutExercise>, IWorkoutExerciseRepository
{
    private readonly FitnessTrackerDbContext _context;

    public WorkoutExerciseRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<WorkoutExercise?> GetByIdAsync(int workoutId, int exerciseId)
    {
        return await _context.WorkoutExercises
        .AsNoTracking()
        .FirstOrDefaultAsync(we =>
            we.WorkoutId == workoutId &&
            we.ExerciseId == exerciseId);
    }

    public async Task DeleteAsync(int workoutId, int exerciseId)
    {
        await _context.WorkoutExercises
        .Where(we => we.WorkoutId == workoutId &&
                     we.ExerciseId == exerciseId)
        .ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(int workoutId, int exerciseId)
    {
        return await _context.WorkoutExercises
            .AsNoTracking()
            .AnyAsync(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId);
    }
}
