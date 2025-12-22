using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IGoalRepository : IGenericRepository<Goal>
{
     
    Task<bool> ExistsOverlappingGoalAsync(
        int userId,
        string goalType,
        DateOnly startDate,
        DateOnly endDate,
        int? ignoreGoalId = null);
}
