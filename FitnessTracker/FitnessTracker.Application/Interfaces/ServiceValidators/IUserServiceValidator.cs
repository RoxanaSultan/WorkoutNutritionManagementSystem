using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IUserServiceValidator
{
    Task<Result> ValidateCreateUserAsync(string email);
    Task<Result> ValidateUpdateUserAsync(int id, string email);
}
