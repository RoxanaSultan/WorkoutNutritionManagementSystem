using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IFoodLogServiceValidator
{
    Task<Result> ValidateCreateAsync(User user, CreateFoodLogDto dto);
    Task<Result> ValidateUpdateAsync(User user, UpdateFoodLogDto dto);
}
