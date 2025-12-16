using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class FoodItemService : GenericService<FoodItem>, IFoodItemService
{
    //private readonly IFoodItemRepository _foodItemRepository;

    public FoodItemService(IFoodItemRepository foodItemRepository)
        : base(foodItemRepository)
    {
        //_foodItemRepository = foodItemRepository;
    }

    //public async Task<Result<FoodItem>> CreateAsync(FoodItem foodItem)
    //{
    //    await _foodItemRepository.CreateAsync(foodItem);
    //    return Result<FoodItem>.Success(foodItem);
    //}

    //public async Task<Result<FoodItem>> DeleteAsync(int id)
    //{
    //    var foodItem = await _foodItemRepository.GetByIdAsync(id);

    //    if (foodItem == null)
    //    {
    //        return Result<FoodItem>.Failure("Food item not found.");
    //    }

    //    await _foodItemRepository.DeleteAsync(foodItem);
    //    return Result<FoodItem>.Success(foodItem);
    //}

    //public async Task<Result<IEnumerable<FoodItem>>> GetAllAsync()
    //{
    //    var foodItems = await _foodItemRepository.GetAllAsync();
    //    return Result<IEnumerable<FoodItem>>.Success(foodItems);
    //}

    //public async Task<Result<FoodItem>> GetByIdAsync(int id)
    //{
    //    var foodItem = await _foodItemRepository.GetByIdAsync(id);

    //    if (foodItem == null)
    //    {
    //        return Result<FoodItem>.Failure("Food item not found.");
    //    }

    //    return Result<FoodItem>.Success(foodItem);
    //}

    //public async Task<Result<FoodItem>> UpdateAsync(int id, FoodItem foodItem)
    //{
    //    if (id != foodItem.Id)
    //    {
    //        return Result<FoodItem>.Failure("Id mismatch.");
    //    }

    //    var existingFoodItem = await _foodItemRepository.GetByIdAsync(id);
    //    if (existingFoodItem == null)
    //    {
    //        return Result<FoodItem>.Failure("Food item not found.");
    //    }

    //    await _foodItemRepository.UpdateAsync(foodItem);
    //    return Result<FoodItem>.Success(foodItem);
    //}
}
