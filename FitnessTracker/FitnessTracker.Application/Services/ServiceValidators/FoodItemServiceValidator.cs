using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services.ServiceValidators;

public class FoodItemServiceValidator : IFoodItemServiceValidator
{
    private readonly IFoodItemRepository _foodItemRepository;

    public FoodItemServiceValidator(IFoodItemRepository foodItemRepository)
    {
        _foodItemRepository = foodItemRepository;
    }

    public async Task<Result> ValidateCreateAsync(CreateFoodItemDto dto)
    {
        var name = dto.Name;
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail(ErrorMessages.FoodItemNameRequired);

        var exists = await _foodItemRepository.ExistsByNameAsync(name);
        if (exists)
            return Result.Fail(ErrorMessages.FoodItemNameAlreadyExists);

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(UpdateFoodItemDto dto, int foodItemId)
    {
        var name = dto.Name;
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail(ErrorMessages.FoodItemNameRequired);

        var exists = await _foodItemRepository.ExistsByNameAsync(name, ignoreId: foodItemId);
        if (exists)
            return Result.Fail(ErrorMessages.FoodItemNameAlreadyExists);

        return Result.Ok();
    }
}
