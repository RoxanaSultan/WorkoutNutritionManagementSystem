using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.Validators;

public class GoalServiceValidator : IGoalServiceValidator
{
    private readonly IGoalRepository _goalRepository;
    public GoalServiceValidator(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<Result> ValidateCreateAsync(User user, CreateGoalDto createGoalDto)
    {
        // StartDate >= RegistrationDate
        var registrationDate = DateOnly.FromDateTime(user.RegistrationDate);
        if (createGoalDto.StartDate < registrationDate)
        {
            return Result.Fail("Start date must be greater than or equal to user's registration date.");
        }

        // StartDate <= EndDate
        if (createGoalDto.EndDate < createGoalDto.StartDate)
        {
            return Result.Fail("End date must be greater than or equal to start date.");
        }

        // Weight loss/gain goals
        var goalType = (createGoalDto.GoalType ?? string.Empty).Trim().ToLowerInvariant();

        if (goalType == "weight loss" && createGoalDto.TargetValue >= user.Weight)
        {
            return Result.Fail("Target value must be less than user's current weight for weight loss goals.");
        }

        if (goalType == "weight gain" && createGoalDto.TargetValue <= user.Weight)
        {
            return Result.Fail("Target value must be greater than user's current weight for weight gain goals.");
        }

        var overlaps = await _goalRepository.ExistsOverlappingGoalAsync(
            createGoalDto.UserId,
            goalType,
            createGoalDto.StartDate,
            createGoalDto.EndDate);

        if (overlaps)
        {
            return Result.Fail("User cannot have another goal of the same type that overlaps the selected period.");
        }

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(User user, UpdateGoalDto updateGoalDto, int goalId)
    {
        var goalType = updateGoalDto.GoalType;
        var startDate = updateGoalDto.StartDate;
        var endDate = updateGoalDto.EndDate;
        var targetValue = updateGoalDto.TargetValue;

        // StartDate >= RegistrationDate
        var registrationDate = DateOnly.FromDateTime(user.RegistrationDate);
        if (startDate < registrationDate)
            return Result.Fail("Start date must be greater than or equal to user's registration date.");

        // StartDate <= EndDate
        if (endDate < startDate)
            return Result.Fail("End date must be greater than or equal to start date.");

        // Weight loss/gain goals
        if (goalType == "weight loss" && targetValue >= user.Weight)
            return Result.Fail("Target value must be less than user's current weight for weight loss goals.");

        if (goalType == "weight gain" && targetValue <= user.Weight)
            return Result.Fail("Target value must be greater than user's current weight for weight gain goals.");

        var overlaps = await _goalRepository.ExistsOverlappingGoalAsync(
            user.Id,
            goalType,
            startDate,
            endDate,
            ignoreGoalId: goalId
        );

        if (overlaps)
        {
            return Result.Fail("User cannot have another goal of the same type that overlaps the selected period.");
        }

        return Result.Ok();
    }
}
