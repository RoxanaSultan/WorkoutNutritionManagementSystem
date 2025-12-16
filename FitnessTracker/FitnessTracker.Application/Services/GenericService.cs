using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _genericRepository;
    private readonly string _entityName;
    public GenericService(IGenericRepository<T> genericRepository)
    {
        _genericRepository = genericRepository;
        _entityName = typeof(T).Name;
    }

    public async Task<Result<T>> CreateAsync(T entity)
    {
        await _genericRepository.CreateAsync(entity);
        return Result<T>.Success(entity);
    }

    public async Task<Result<T>> DeleteAsync(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);

        if (entity == null)
        {
            return Result<T>.Failure($"{_entityName} not found.");
        }

        await _genericRepository.DeleteAsync(id);
        return Result<T>.Success(entity);
    }

    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        var entities = await _genericRepository.GetAllAsync();
        return Result<IEnumerable<T>>.Success(entities);
    }

    public async Task<Result<T>> GetByIdAsync(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);

        if (entity == null)
        {
            return Result<T>.Failure($"{_entityName} not found.");
        }

        return Result<T>.Success(entity);
    }

    public async Task<Result<T>> UpdateAsync(int id, T entity)
    {
        var existingEntity = await _genericRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            return Result<T>.Failure($"{_entityName} not found.");
        }

        await _genericRepository.UpdateAsync(entity);
        return Result<T>.Success(entity);
    }
}
