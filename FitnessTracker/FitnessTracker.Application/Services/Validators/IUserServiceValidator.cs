using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services.Validators;

public interface IUserServiceValidator
{
    Task<Result> ValidateCreateUserAsync(string email);
    Task<Result> ValidateUpdateUserAsync(int id, string email);
}
