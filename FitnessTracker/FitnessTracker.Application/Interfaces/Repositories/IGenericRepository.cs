namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task CreateAsync(T exercise);
    Task UpdateAsync(T exercise);
    Task DeleteAsync(int id);
}
