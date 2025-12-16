using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;

namespace FitnessTracker.Infrastructure.Repositories;

public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
{
    //private readonly FitnessTrackerDbContext _context;
    public WorkoutRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        //_context = context;
    }

    //public async Task<IEnumerable<Workout>> GetAllAsync()
    //{
    //    return await _context.Workouts.ToListAsync();
    //}

    //public async Task<Workout?> GetByIdAsync(int id)
    //{
    //    return await _context.Workouts.FindAsync(id);
    //}

    //public async Task CreateAsync(Workout workout)
    //{
    //    _context.Exercises.Add(workout);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task UpdateAsync(Workout workout)
    //{
    //    _context.Entry(workout).State = EntityState.Modified;
    //    await _context.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(Workout workout)
    //{
    //    _context.Exercises.Remove(workout);
    //    await _context.SaveChangesAsync();
    //}
}
