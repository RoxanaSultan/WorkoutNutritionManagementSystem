using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<User>>> GetAllAsync();
    Task<Result<User>> GetByIdAsync(int id);
    Task<Result<User>> CreateAsync(User user);
    Task<Result<User>> UpdateAsync(int id, User user);
    Task<Result<User>> DeleteAsync(int id);
}
