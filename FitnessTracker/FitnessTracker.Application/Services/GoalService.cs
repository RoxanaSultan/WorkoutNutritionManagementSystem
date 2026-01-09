using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class GoalService : IGoalService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGoalServiceValidator _goalServiceValidator;

    public GoalService(IUnitOfWork unitOfWork, IMapper mapper, IGoalServiceValidator goalServiceValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _goalServiceValidator = goalServiceValidator;
    }

    public async Task<Result<IEnumerable<GoalDto>>> GetAllAsync()
    {
        var goals = await _unitOfWork.Goals.GetAllAsync();
        var goalsDto = _mapper.Map<IEnumerable<GoalDto>>(goals);

        return Result<IEnumerable<GoalDto>>.Ok(goalsDto);
    }

    public async Task<Result<GoalDto>> GetByIdAsync(int id)
    {
        var goal = await _unitOfWork.Goals.GetByIdAsync(id);

        if (goal == null)
        {
            return Result<GoalDto>.Fail(ErrorMessages.GoalNotFound);
        }

        var goalDto = _mapper.Map<GoalDto>(goal);

        return Result<GoalDto>.Ok(goalDto);
    }

    public async Task<Result<GoalDto>> CreateAsync(CreateGoalDto createGoalDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(createGoalDto.UserId);

        if (user == null)
        {
            return Result<GoalDto>.Fail(ErrorMessages.UserWithIdNotFound(createGoalDto.UserId));
        }

        var validation = await _goalServiceValidator.ValidateCreateAsync(user, createGoalDto);

        if (!validation.Success)
        {
            return Result<GoalDto>.Fail(validation.Error);
        }

        var goal = _mapper.Map<Goal>(createGoalDto);

        await _unitOfWork.Goals.CreateAsync(goal);
        await _unitOfWork.SaveChangesAsync();

        var goalDto = _mapper.Map<GoalDto>(goal);

        return Result<GoalDto>.Ok(goalDto);
    }

    public async Task<Result<GoalDto>> UpdateAsync(int id, UpdateGoalDto updateGoalDto)
    {
        var existingGoal = await _unitOfWork.Goals.GetByIdAsync(id);

        if (existingGoal == null)
            return Result<GoalDto>.Fail(ErrorMessages.GoalNotFound);

        var user = await _unitOfWork.Users.GetByIdAsync(existingGoal.UserId);

        if (user == null)
        {
            return Result<GoalDto>.Fail(ErrorMessages.UserNotFound);
        }

        var validation = await _goalServiceValidator.ValidateUpdateAsync(user, updateGoalDto, id);

        if (!validation.Success)
        {
            return Result<GoalDto>.Fail(validation.Error);
        }

        _mapper.Map(updateGoalDto, existingGoal);

        await _unitOfWork.Goals.UpdateAsync(existingGoal);
        await _unitOfWork.SaveChangesAsync();

        var goalDto = _mapper.Map<GoalDto>(existingGoal);

        return Result<GoalDto>.Ok(goalDto);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var exists = await _unitOfWork.Goals.ExistsAsync(id);

        if (!exists)
        {
            return Result.Fail(ErrorMessages.GoalNotFound);
        }

        await _unitOfWork.Goals.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
