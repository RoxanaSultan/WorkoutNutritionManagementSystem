using FitnessTracker.Application.DTOs.User;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<IEnumerable<UserDto>>> GetAllAsync();
    Task<Result<UserDto>> GetByIdAsync(int id);
    Task<Result<UserDto>> CreateAsync(CreateUserDto createUserDto);
    Task<Result<UserDto>> UpdateAsync(int id, UpdateUserDto updateUserDto);
    Task<Result> DeleteAsync(int id);
}
