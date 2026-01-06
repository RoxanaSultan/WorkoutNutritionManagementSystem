using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class FoodLogService : IFoodLogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFoodLogServiceValidator _foodLogServiceValidator;

    public FoodLogService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFoodLogServiceValidator foodLogServiceValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _foodLogServiceValidator = foodLogServiceValidator;
    }

    public async Task<Result<IEnumerable<FoodLogDto>>> GetAllAsync()
    {
        var foodLogs = await _unitOfWork.FoodLogs.GetAllAsync();
        var foodLogDtos = _mapper.Map<IEnumerable<FoodLogDto>>(foodLogs);

        return Result<IEnumerable<FoodLogDto>>.Ok(foodLogDtos);
    }

    public async Task<Result<FoodLogDto>> GetByIdAsync(int id)
    {
        var foodLog = await _unitOfWork.FoodLogs.GetByIdAsync(id);

        if (foodLog == null)
        {
            return Result<FoodLogDto>.Fail(ErrorMessages.FoodLogNotFound);
        }

        var foodLogDto = _mapper.Map<FoodLogDto>(foodLog);

        return Result<FoodLogDto>.Ok(foodLogDto);
    }

    public async Task<Result<FoodLogDto>> CreateAsync(CreateFoodLogDto createFoodLogDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(createFoodLogDto.UserId);

        if (user == null)
        {
            // varianta cu id (dacă adaugi metoda statică în ErrorMessages)
            return Result<FoodLogDto>.Fail(ErrorMessages.UserWithIdNotFound(createFoodLogDto.UserId));

            // sau, dacă vrei fără id:
            // return Result<FoodLogDto>.Fail(ErrorMessages.UserNotFound);
        }

        var validation = await _foodLogServiceValidator.ValidateCreateAsync(user, createFoodLogDto);

        if (!validation.Success)
        {
            return Result<FoodLogDto>.Fail(validation.Error);
        }

        var foodLog = _mapper.Map<FoodLog>(createFoodLogDto);

        await _unitOfWork.FoodLogs.CreateAsync(foodLog);
        await _unitOfWork.SaveChangesAsync();

        var foodLogDto = _mapper.Map<FoodLogDto>(foodLog);

        return Result<FoodLogDto>.Ok(foodLogDto);
    }

    public async Task<Result<FoodLogDto>> UpdateAsync(int id, UpdateFoodLogDto updateFoodLogDto)
    {
        var existingFoodLog = await _unitOfWork.FoodLogs.GetByIdAsync(id);

        if (existingFoodLog == null)
        {
            return Result<FoodLogDto>.Fail(ErrorMessages.FoodLogNotFound);
        }

        var user = await _unitOfWork.Users.GetByIdAsync(existingFoodLog.UserId);

        if (user == null)
        {
            return Result<FoodLogDto>.Fail(ErrorMessages.UserNotFound);
        }

        var foodItem = await _unitOfWork.FoodItems.GetByIdAsync(existingFoodLog.FoodId);

        if (foodItem == null)
        {
            return Result<FoodLogDto>.Fail(ErrorMessages.FoodItemNotFound);
        }

        var validation = await _foodLogServiceValidator.ValidateUpdateAsync(user, updateFoodLogDto);

        if (!validation.Success)
        {
            return Result<FoodLogDto>.Fail(validation.Error);
        }

        _mapper.Map(updateFoodLogDto, existingFoodLog);

        await _unitOfWork.FoodLogs.UpdateAsync(existingFoodLog);
        await _unitOfWork.SaveChangesAsync();

        var foodLogDto = _mapper.Map<FoodLogDto>(existingFoodLog);

        return Result<FoodLogDto>.Ok(foodLogDto);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var exists = await _unitOfWork.FoodLogs.ExistsAsync(id);

        if (!exists)
        {
            return Result.Fail(ErrorMessages.FoodLogNotFound);
        }

        await _unitOfWork.FoodLogs.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
