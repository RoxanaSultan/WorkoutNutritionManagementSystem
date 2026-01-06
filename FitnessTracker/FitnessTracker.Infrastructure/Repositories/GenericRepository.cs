using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly FitnessTrackerDbContext _context;
    public GenericRepository(FitnessTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context
            .Set<T>()
            .AnyAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task CreateAsync(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        await _context
            .Set<T>()
            .Where(e => EF.Property<int>(e, "Id") == id)
            .ExecuteDeleteAsync();
    }
}
