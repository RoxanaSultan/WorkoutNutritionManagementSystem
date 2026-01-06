using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
{
    private readonly FitnessTrackerDbContext _context;

    public WorkoutRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsSameStartTimeAsync(int userId, DateTime dateToMinute, int? ignoreWorkoutId = null)
    {
        var query = _context.Workouts.Where(w => w.UserId == userId);

        if (ignoreWorkoutId.HasValue)
            query = query.Where(w => w.Id != ignoreWorkoutId.Value);

        return await query.AnyAsync(w =>
            w.Date.Year == dateToMinute.Year &&
            w.Date.Month == dateToMinute.Month &&
            w.Date.Day == dateToMinute.Day &&
            w.Date.Hour == dateToMinute.Hour &&
            w.Date.Minute == dateToMinute.Minute
        );
    }

    public async Task<bool> WorkoutExerciseExistsAsync(int workoutId, int exerciseId)
    {
        return await _context.WorkoutExercises
            .AsNoTracking()
            .AnyAsync(x => x.WorkoutId == workoutId && x.ExerciseId == exerciseId);
    }

    public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercisesByWorkoutIdAsync(int workoutId)
    {
        return await _context.WorkoutExercises
            .Where(we => we.WorkoutId == workoutId)
            .ToListAsync();
    }

    public async Task UpdateWorkoutExerciseAsync(WorkoutExercise entity)
    {
        _context.WorkoutExercises.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddWorkoutExerciseAsync(WorkoutExercise entity)
    {
        _context.WorkoutExercises.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<WorkoutExercise?> GetWorkoutExerciseAsync(int workoutId, int exerciseId)
    {
        return await _context.WorkoutExercises
            .FirstOrDefaultAsync(x => x.WorkoutId == workoutId && x.ExerciseId == exerciseId);
    }

    public async Task RemoveWorkoutExerciseAsync(WorkoutExercise entity)
    {
        _context.WorkoutExercises.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<int>> GetWorkoutIdsByUserIdAsync(int userId)
    {
        return await _context.Workouts
            .Where(w => w.UserId == userId)
            .Select(w => w.Id)
            .ToListAsync();
    }

    public async Task DeleteByUserIdAsync(int userId)
    {
        await _context.Workouts
            .Where(w => w.UserId == userId)
            .ExecuteDeleteAsync();
    }

    public async Task DeleteByWorkoutIdsAsync(IEnumerable<int> workoutIds)
    {
        await _context.WorkoutExercises
            .Where(we => workoutIds.Contains(we.WorkoutId))
            .ExecuteDeleteAsync();
    }
}
