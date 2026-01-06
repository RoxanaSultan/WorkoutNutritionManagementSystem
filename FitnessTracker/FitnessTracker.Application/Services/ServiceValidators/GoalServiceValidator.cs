using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.ServiceValidators;

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
            return Result.Fail(ErrorMessages.GoalStartDateBeforeUserRegistration);
        }

        // StartDate <= EndDate
        if (createGoalDto.EndDate < createGoalDto.StartDate)
        {
            return Result.Fail(ErrorMessages.GoalEndDateBeforeStartDate);
        }

        // Weight loss/gain goals
        var goalType = (createGoalDto.GoalType ?? string.Empty).Trim().ToLowerInvariant();

        if (goalType == "weight loss" && createGoalDto.TargetValue >= user.Weight)
        {
            return Result.Fail(ErrorMessages.GoalTargetValueInvalidForWeightLoss);
        }

        if (goalType == "weight gain" && createGoalDto.TargetValue <= user.Weight)
        {
            return Result.Fail(ErrorMessages.GoalTargetValueInvalidForWeightGain);
        }

        var overlaps = await _goalRepository.ExistsOverlappingGoalAsync(
            createGoalDto.UserId,
            goalType,
            createGoalDto.StartDate,
            createGoalDto.EndDate);

        if (overlaps)
        {
            return Result.Fail(ErrorMessages.GoalOverlapsSameType);
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
            return Result.Fail(ErrorMessages.GoalStartDateBeforeUserRegistration);

        // StartDate <= EndDate
        if (endDate < startDate)
            return Result.Fail(ErrorMessages.GoalEndDateBeforeStartDate);

        // Weight loss/gain goals
        if (goalType == "weight loss" && targetValue >= user.Weight)
            return Result.Fail(ErrorMessages.GoalTargetValueInvalidForWeightLoss);

        if (goalType == "weight gain" && targetValue <= user.Weight)
            return Result.Fail(ErrorMessages.GoalTargetValueInvalidForWeightGain);

        var overlaps = await _goalRepository.ExistsOverlappingGoalAsync(
            user.Id,
            goalType,
            startDate,
            endDate,
            ignoreGoalId: goalId
        );

        if (overlaps)
        {
            return Result.Fail(ErrorMessages.GoalOverlapsSameType);
        }

        return Result.Ok();
    }
}
