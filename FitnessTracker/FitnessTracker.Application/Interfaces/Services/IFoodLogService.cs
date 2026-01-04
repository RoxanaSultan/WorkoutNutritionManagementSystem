using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IFoodLogService
{
    Task<Result<IEnumerable<FoodLogDto>>> GetAllAsync();
    Task<Result<FoodLogDto>> GetByIdAsync(int id);
    Task<Result<FoodLogDto>> CreateAsync(CreateFoodLogDto createDto);
    Task<Result<FoodLogDto>> UpdateAsync(int id, UpdateFoodLogDto updateDto);
    Task<Result<FoodLogDto>> DeleteAsync(int id);
}
