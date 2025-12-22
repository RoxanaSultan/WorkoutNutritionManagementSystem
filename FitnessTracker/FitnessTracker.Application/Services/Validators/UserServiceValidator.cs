using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services.Validators;

public class UserServiceValidator : IUserServiceValidator
{
    private readonly IUserRepository _userRepository;

    public UserServiceValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> ValidateCreateUserAsync(string email)
    {
        var isUnique = await IsEmailUniqueAsync(email);
        if (!isUnique)
            return Result.Fail($"User with email {email} already exists.");

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateUserAsync(int id, string email)
    {
        //var userExists = await DoesUserExistAsync(id);
        //if (!userExists)
        //    return Result.Fail("User not found.");

        var isUnique = await IsEmailUniqueForUpdateAsync(id, email);
        if (!isUnique)
        {
            return Result.Fail($"User with email {email} already exists.");
        }

        return Result.Ok();
    }

    private async Task<bool> IsEmailUniqueAsync(string email)
        => !await _userRepository.ExistsByEmailAsync(email);

    private async Task<bool> IsEmailUniqueForUpdateAsync(int id, string email)
        => !await _userRepository.ExistsByEmailExceptUserIdAsync(email, id);

    //private async Task<bool> DoesUserExistAsync(int id)
    //    => await _userRepository.GetByIdAsync(id) != null;
}