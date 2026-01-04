using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IGoalService
{
    Task<Result<IEnumerable<GoalDto>>> GetAllAsync();
    Task<Result<GoalDto>> GetByIdAsync(int id);
    Task<Result<GoalDto>> CreateAsync(CreateGoalDto createGoalDto);
    Task<Result<GoalDto>> UpdateAsync(int id, UpdateGoalDto updateGoalDto);
    Task<Result<GoalDto>> DeleteAsync(int id);
}
