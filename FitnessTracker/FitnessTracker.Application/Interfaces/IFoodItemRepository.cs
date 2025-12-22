using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IFoodItemRepository : IGenericRepository<FoodItem>
{
    Task<bool> ExistsByNameAsync(string normalizedName, int? ignoreId = null);
}
