using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.Validators;

public class FoodLogServiceValidator : IFoodLogServiceValidator
{
    public Task<Result> ValidateCreateAsync(User user, CreateFoodLogDto dto)
        => Task.FromResult(ValidateCommon(user, dto.LogDate));

    public Task<Result> ValidateUpdateAsync(User user, UpdateFoodLogDto dto)
        => Task.FromResult(ValidateCommon(user, dto.LogDate));

    private static Result ValidateCommon(User user, DateTime logDate)
    {
        var registrationDate = DateOnly.FromDateTime(user.RegistrationDate);
        var logDateOnly = DateOnly.FromDateTime(logDate);

        if (logDateOnly < registrationDate)
            return Result.Fail("User can't log food before their registration date.");

        return Result.Ok();
    }
}