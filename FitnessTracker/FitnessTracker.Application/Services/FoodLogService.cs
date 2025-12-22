using AutoMapper;
using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Application.Services.Validators;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class FoodLogService : IFoodLogService
{
    private readonly IFoodLogRepository _foodLogRepository;
    private readonly IUserRepository _userRepository;
    private readonly IFoodItemRepository _foodItemRepository;
    private readonly IMapper _mapper;
    private readonly IFoodLogServiceValidator _foodLogServiceValidator;

    public FoodLogService(
        IFoodLogRepository foodLogRepository,
        IUserRepository userRepository,
        IFoodItemRepository foodItemRepository,
        IMapper mapper,
        IFoodLogServiceValidator foodLogServiceValidator)
    {
        _foodLogRepository = foodLogRepository;
        _userRepository = userRepository;
        _foodItemRepository = foodItemRepository;
        _mapper = mapper;
        _foodLogServiceValidator = foodLogServiceValidator;
    }

    public async Task<Result<IEnumerable<FoodLogDto>>> GetAllAsync()
    {
        var logs = await _foodLogRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<FoodLogDto>>(logs);
        return Result<IEnumerable<FoodLogDto>>.Ok(dtos);
    }

    public async Task<Result<FoodLogDto>> GetByIdAsync(int id)
    {
        var log = await _foodLogRepository.GetByIdAsync(id);
        if (log == null)
        {
            return Result<FoodLogDto>.Fail("Food log not found.");
        }

        var dto = _mapper.Map<FoodLogDto>(log);
        return Result<FoodLogDto>.Ok(dto);
    }

    public async Task<Result<FoodLogDto>> CreateAsync(CreateFoodLogDto createDto)
    {
        var user = await _userRepository.GetByIdAsync(createDto.UserId);
        if (user == null)
        {
            return Result<FoodLogDto>.Fail($"User with id {createDto.UserId} not found.");
        }

        var validation = await _foodLogServiceValidator.ValidateCreateAsync(user, createDto);
        if (!validation.Success)
        {
            return Result<FoodLogDto>.Fail(validation.Error);
        }

        var foodLog = _mapper.Map<FoodLog>(createDto);
        await _foodLogRepository.CreateAsync(foodLog);

        var foodLogDto = _mapper.Map<FoodLogDto>(foodLog);
        return Result<FoodLogDto>.Ok(foodLogDto);
    }

    public async Task<Result<FoodLogDto>> UpdateAsync(int id, UpdateFoodLogDto updateDto)
    {
        var existingFoodLog = await _foodLogRepository.GetByIdAsync(id);
        if (existingFoodLog == null)
        {
            return Result<FoodLogDto>.Fail("Food log not found.");
        }

        var user = await _userRepository.GetByIdAsync(updateDto.UserId);
        if (user == null)
        {
            return Result<FoodLogDto>.Fail("User not found.");
        }

        var foodItem = await _foodItemRepository.GetByIdAsync(updateDto.FoodId);
        if (foodItem == null)
        {
            return Result<FoodLogDto>.Fail("Food item not found.");
        }

        var validation = await _foodLogServiceValidator.ValidateUpdateAsync(user, updateDto);
        if (!validation.Success)
        {
            return Result<FoodLogDto>.Fail(validation.Error);
        }

        _mapper.Map(updateDto, existingFoodLog);
        await _foodLogRepository.UpdateAsync(existingFoodLog);

        var foodLogDto = _mapper.Map<FoodLogDto>(existingFoodLog);
        return Result<FoodLogDto>.Ok(foodLogDto);
    }

    public async Task<Result<FoodLogDto>> DeleteAsync(int id)
    {
        var foodLog = await _foodLogRepository.GetByIdAsync(id);
        if (foodLog == null)
        {
            return Result<FoodLogDto>.Fail("Food log not found.");
        }

        await _foodLogRepository.DeleteAsync(id);

        var foodLogDto = _mapper.Map<FoodLogDto>(foodLog);
        return Result<FoodLogDto>.Ok(foodLogDto);
    }
}