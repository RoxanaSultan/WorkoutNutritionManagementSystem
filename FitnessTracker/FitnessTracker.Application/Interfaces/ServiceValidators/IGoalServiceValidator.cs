using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IGoalServiceValidator
{
    Task<Result> ValidateCreateAsync(User user, CreateGoalDto dto);
    Task<Result> ValidateUpdateAsync(User user, UpdateGoalDto dto, int goalId);
}
