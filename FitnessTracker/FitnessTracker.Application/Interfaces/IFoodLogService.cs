using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IFoodLogService
{
    Task<Result<IEnumerable<FoodLogDto>>> GetAllAsync();
    Task<Result<FoodLogDto>> GetByIdAsync(int id);
    Task<Result<FoodLogDto>> CreateAsync(CreateFoodLogDto createDto);
    Task<Result<FoodLogDto>> UpdateAsync(int id, UpdateFoodLogDto updateDto);
    Task<Result<FoodLogDto>> DeleteAsync(int id);
}
