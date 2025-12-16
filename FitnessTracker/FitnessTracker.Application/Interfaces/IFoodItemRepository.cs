using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IFoodItemRepository : IGenericRepository<FoodItem>
{
    //Task<IEnumerable<FoodItem>> GetAllAsync();
    //Task<FoodItem?> GetByIdAsync(int id);
    //Task CreateAsync(FoodItem foodItem);
    //Task UpdateAsync(FoodItem foodItem);
    //Task DeleteAsync(FoodItem foodItem);
}
