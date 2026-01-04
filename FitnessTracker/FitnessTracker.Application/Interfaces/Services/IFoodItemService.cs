using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IFoodItemService
{
    Task<Result<IEnumerable<FoodItemDto>>> GetAllAsync();
    Task<Result<FoodItemDto>> GetByIdAsync(int id);
    Task<Result<FoodItemDto>> CreateAsync(CreateFoodItemDto createDto);
    Task<Result<FoodItemDto>> UpdateAsync(int id, UpdateFoodItemDto updateDto);
    Task<Result<FoodItemDto>> DeleteAsync(int id);
}