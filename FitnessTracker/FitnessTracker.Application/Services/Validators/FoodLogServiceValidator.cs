using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.Validators;

public class FoodLogServiceValidator : IFoodLogServiceValidator
{
    public async Task<Result> ValidateCreateAsync(User user, CreateFoodLogDto dto)
    {
        var registrationDate = DateOnly.FromDateTime(user.RegistrationDate);
        var logDateOnly = DateOnly.FromDateTime(dto.LogDate);

        if (logDateOnly < registrationDate)
            return Result.Fail("User can't log food before their registration date.");

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(User user, UpdateFoodLogDto dto)
    {
        var registrationDate = DateOnly.FromDateTime(user.RegistrationDate);
        var logDateOnly = DateOnly.FromDateTime(dto.LogDate);

        if (logDateOnly < registrationDate)
            return Result.Fail("User can't log food before their registration date.");

        return Result.Ok();
    }
}