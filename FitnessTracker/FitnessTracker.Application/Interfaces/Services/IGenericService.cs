using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IGenericService<T> where T : class
{
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> GetByIdAsync(int id);
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(int id, T entity);
    Task<Result<T>> DeleteAsync(int id);
}
