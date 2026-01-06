using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IFoodLogRepository : IGenericRepository<FoodLog>
{
    Task DeleteByUserIdAsync(int userId);
}
