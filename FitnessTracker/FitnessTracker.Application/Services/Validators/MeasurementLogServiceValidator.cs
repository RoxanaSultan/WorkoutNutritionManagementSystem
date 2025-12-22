using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.Validators;

public class MeasurementLogServiceValidator : IMeasurementLogServiceValidator
{
    private const decimal MaxKgPerDay = 3.0m;

    public Task<Result> ValidateCreateAsync(MeasurementLog? previous, MeasurementLog current)
        => Task.FromResult(ValidateWeightRate(previous, current));

    public Task<Result> ValidateUpdateAsync(MeasurementLog? previous, MeasurementLog current)
        => Task.FromResult(ValidateWeightRate(previous, current));

    private static Result ValidateWeightRate(MeasurementLog? previous, MeasurementLog current)
    {
        if (previous == null) return Result.Ok();

        var days = (current.Date.Date - previous.Date.Date).Days;
        if (days <= 0) return Result.Ok();

        var diffKg = Math.Abs(current.Weight - previous.Weight);
        var kgPerDay = diffKg / days;

        if (kgPerDay > MaxKgPerDay)
        {
            return Result.Fail($"Weight change is too large: {kgPerDay:0.##} kg/day (max {MaxKgPerDay} kg/day).");
        }

        return Result.Ok();
    }
}