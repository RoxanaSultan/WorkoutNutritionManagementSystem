using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services.Validators;

public class FoodItemServiceValidator : IFoodItemServiceValidator
{
    private readonly IFoodItemRepository _foodItemRepository;

    public FoodItemServiceValidator(IFoodItemRepository foodItemRepository)
    {
        _foodItemRepository = foodItemRepository;
    }

    public async Task<Result> ValidateCreateAsync(CreateFoodItemDto dto)
    {
        var name = NormalizeName(dto.Name);
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Name is required.");

        // no duplicates
        var exists = await _foodItemRepository.ExistsByNameAsync(name);
        if (exists)
            return Result.Fail("A food item with the same name already exists.");

        // per 100g validations (basic)
        var macrosValidation = ValidatePer100g(dto.Calories, dto.Protein, dto.Carbs, dto.Fat);
        if (!macrosValidation.Success)
            return macrosValidation;

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(UpdateFoodItemDto dto, int foodItemId)
    {
        var name = NormalizeName(dto.Name);
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Name is required.");

        // no duplicates (ignore current id)
        var exists = await _foodItemRepository.ExistsByNameAsync(name, ignoreId: foodItemId);
        if (exists)
            return Result.Fail("A food item with the same name already exists.");

        var macrosValidation = ValidatePer100g(dto.Calories, dto.Protein, dto.Carbs, dto.Fat);
        if (!macrosValidation.Success)
            return macrosValidation;

        return Result.Ok();
    }

    private static string NormalizeName(string? name)
        => (name ?? string.Empty).Trim().ToLowerInvariant();

    private static Result ValidatePer100g(int? calories, decimal? protein, decimal? carbs, decimal? fat)
    {
        // valori nenegative
        if (calories is < 0) return Result.Fail("Calories cannot be negative (per 100g).");
        if (protein is < 0) return Result.Fail("Protein cannot be negative (per 100g).");
        if (carbs is < 0) return Result.Fail("Carbs cannot be negative (per 100g).");
        if (fat is < 0) return Result.Fail("Fat cannot be negative (per 100g).");

        // opțional: limite maxime “realiste” pentru 100g (ca să prinzi inputuri greșite, ex: per porție)
        if (calories is > 900) return Result.Fail("Calories seems too high for 100g (max 900).");
        if (protein is > 100) return Result.Fail("Protein cannot exceed 100g per 100g product.");
        if (carbs is > 100) return Result.Fail("Carbs cannot exceed 100g per 100g product.");
        if (fat is > 100) return Result.Fail("Fat cannot exceed 100g per 100g product.");

        return Result.Ok();
    }
}
