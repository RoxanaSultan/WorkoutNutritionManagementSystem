using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;

namespace FitnessTracker.Infrastructure.Repositories;

public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
{
    //private readonly FitnessTrackerDbContext _context;
    public ExerciseRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        //_context = context;
    }

    //public async Task<IEnumerable<Exercise>> GetAllAsync()
    //{
    //    return await _context.Exercises.ToListAsync();
    //}

    //public async Task<Exercise?> GetByIdAsync(int id)
    //{
    //    return await _context.Exercises.FindAsync(id);
    //}

    //public async Task CreateAsync(Exercise exercise)
    //{
    //    _context.Exercises.Add(exercise);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task UpdateAsync(Exercise exercise)
    //{
    //    _context.Entry(exercise).State = EntityState.Modified;
    //    await _context.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(Exercise exercise)
    //{
    //    _context.Exercises.Remove(exercise);
    //    await _context.SaveChangesAsync();
    //}
}
