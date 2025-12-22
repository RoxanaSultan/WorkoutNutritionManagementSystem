using AutoMapper;
using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Application.Services.Validators;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class GoalService : IGoalService
{
    private readonly IGoalRepository _goalRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IGoalServiceValidator _goalServiceValidator;
    public GoalService(IGoalRepository goalRepository, IUserRepository userRepository, IMapper mapper, IGoalServiceValidator goalServiceValidator)
    {
        _goalRepository = goalRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _goalServiceValidator = goalServiceValidator;
    }

    public async Task<Result<IEnumerable<GoalDto>>> GetAllAsync()
    {
        var goals = await _goalRepository.GetAllAsync();
        var goalsDto = _mapper.Map<IEnumerable<GoalDto>>(goals);

        return Result<IEnumerable<GoalDto>>.Ok(goalsDto);
    }

    public async Task<Result<GoalDto>> GetByIdAsync(int id)
    {
        var goal = await _goalRepository.GetByIdAsync(id);

        if (goal == null)
        {
            return Result<GoalDto>.Fail("Goal not found.");
        }

        var goalDto = _mapper.Map<GoalDto>(goal);

        return Result<GoalDto>.Ok(goalDto);
    }

    public async Task<Result<GoalDto>> CreateAsync(CreateGoalDto createGoalDto)
    {
        var user = await _userRepository.GetByIdAsync(createGoalDto.UserId);
        if (user == null)
        {
            return Result<GoalDto>.Fail($"User with id {createGoalDto.UserId} not found.");
        }

        var validation = await _goalServiceValidator.ValidateCreateAsync(user, createGoalDto);
        if (!validation.Success)
        {
            return Result<GoalDto>.Fail(validation.Error);
        }

        var goal = _mapper.Map<Goal>(createGoalDto);
        await _goalRepository.CreateAsync(goal);

        var goalDto = _mapper.Map<GoalDto>(goal);
        return Result<GoalDto>.Ok(goalDto);
    }

    public async Task<Result<GoalDto>> UpdateAsync(int id, UpdateGoalDto updateGoalDto)
    {
        var existingGoal = await _goalRepository.GetByIdAsync(id);
        if (existingGoal == null)
            return Result<GoalDto>.Fail("Goal not found.");

        var user = await _userRepository.GetByIdAsync(updateGoalDto.UserId);
        if (user == null)
        {
            return Result<GoalDto>.Fail("User not found.");
        }

        var validation = await _goalServiceValidator.ValidateUpdateAsync(user, updateGoalDto, id);
        if (!validation.Success)
        {
            return Result<GoalDto>.Fail(validation.Error);
        }

        _mapper.Map(updateGoalDto, existingGoal);
        await _goalRepository.UpdateAsync(existingGoal);

        var goalDto = _mapper.Map<GoalDto>(existingGoal);
        return Result<GoalDto>.Ok(goalDto);
    }


    public async Task<Result<GoalDto>> DeleteAsync(int id)
    {
        var goal = await _goalRepository.GetByIdAsync(id);
        if (goal == null)
        {
            return Result<GoalDto>.Fail("Goal not found.");
        }

        await _goalRepository.DeleteAsync(id);

        var goalDto = _mapper.Map<GoalDto>(goal);
        return Result<GoalDto>.Ok(goalDto);
    }
}
