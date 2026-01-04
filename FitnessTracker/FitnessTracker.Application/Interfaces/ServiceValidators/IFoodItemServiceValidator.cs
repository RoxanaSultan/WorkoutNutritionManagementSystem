using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IFoodItemServiceValidator
{
    Task<Result> ValidateCreateAsync(CreateFoodItemDto dto);
    Task<Result> ValidateUpdateAsync(UpdateFoodItemDto dto, int foodItemId);
}
