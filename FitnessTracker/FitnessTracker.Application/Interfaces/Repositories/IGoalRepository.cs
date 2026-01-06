using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IGoalRepository : IGenericRepository<Goal>
{
    Task DeleteByUserIdAsync(int userId);
    Task<bool> ExistsOverlappingGoalAsync(
        int userId,
        string goalType,
        DateOnly startDate,
        DateOnly endDate,
        int? ignoreGoalId = null);
}
