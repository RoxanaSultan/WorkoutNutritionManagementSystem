using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class UserService : GenericService<User>, IUserService
{
    //private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
        : base(userRepository)
    {
        //_userRepository = userRepository;
    }

    //public async Task<Result<User>> CreateAsync(User user)
    //{
    //    await _userRepository.CreateAsync(user);
    //    return Result<User>.Success(user);
    //}

    //public async Task<Result<User>> DeleteAsync(int id)
    //{
    //    var user = await _userRepository.GetByIdAsync(id);

    //    if (user == null)
    //    {
    //        return Result<User>.Failure("User not found.");
    //    }

    //    await _userRepository.DeleteAsync(user);
    //    return Result<User>.Success(user);
    //}

    //public async Task<Result<IEnumerable<User>>> GetAllAsync()
    //{
    //    var users = await _userRepository.GetAllAsync();
    //    return Result<IEnumerable<User>>.Success(users);
    //}

    //public async Task<Result<User>> GetByIdAsync(int id)
    //{
    //    var user = await _userRepository.GetByIdAsync(id);

    //    if (user == null)
    //    {
    //        return Result<User>.Failure("User not found.");
    //    }

    //    return Result<User>.Success(user);
    //}

    //public async Task<Result<User>> UpdateAsync(int id, User user)
    //{
    //    if (id != user.Id)
    //    {
    //        return Result<User>.Failure("Id mismatch.");
    //    }

    //    var existingUser = await _userRepository.GetByIdAsync(id);
    //    if (existingUser == null)
    //    {
    //        return Result<User>.Failure("User not found.");
    //    }

    //    await _userRepository.UpdateAsync(user);
    //    return Result<User>.Success(user);
    //}
}
