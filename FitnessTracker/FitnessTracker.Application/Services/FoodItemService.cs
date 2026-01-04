using AutoMapper;
using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class FoodItemService : IFoodItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFoodItemServiceValidator _validator;

    public FoodItemService(
        IUnitOfWork unitOfWork, IMapper mapper, IFoodItemServiceValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<FoodItemDto>>> GetAllAsync()
    {
        var foodItems = await _unitOfWork.FoodItems.GetAllAsync();
        var foodItemsDtos = _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);
        return Result<IEnumerable<FoodItemDto>>.Ok(foodItemsDtos);
    }

    public async Task<Result<FoodItemDto>> GetByIdAsync(int id)
    {
        var foodItem = await _unitOfWork.FoodItems.GetByIdAsync(id);
        if (foodItem == null)
        {
            return Result<FoodItemDto>.Fail("Food item not found.");
        }

        var foodItemDto = _mapper.Map<FoodItemDto>(foodItem);
        return Result<FoodItemDto>.Ok(foodItemDto);
    }

    public async Task<Result<FoodItemDto>> CreateAsync(CreateFoodItemDto createFoodItemDto)
    {
        var validation = await _validator.ValidateCreateAsync(createFoodItemDto);
        if (!validation.Success)
        {
            return Result<FoodItemDto>.Fail(validation.Error);
        }

        var foodItem = _mapper.Map<FoodItem>(createFoodItemDto);
        await _unitOfWork.FoodItems.CreateAsync(foodItem);
        await _unitOfWork.SaveChangesAsync();

        var foodItemDto = _mapper.Map<FoodItemDto>(foodItem);
        return Result<FoodItemDto>.Ok(foodItemDto);
    }

    public async Task<Result<FoodItemDto>> UpdateAsync(int id, UpdateFoodItemDto updateFoodItemDto)
    {
        var existingFoodItem = await _unitOfWork.FoodItems.GetByIdAsync(id);
        if (existingFoodItem == null)
        {
            return Result<FoodItemDto>.Fail("Food item not found.");
        }

        var validation = await _validator.ValidateUpdateAsync(updateFoodItemDto, id);
        if (!validation.Success)
        {
            return Result<FoodItemDto>.Fail(validation.Error);
        }

        _mapper.Map(updateFoodItemDto, existingFoodItem);
        await _unitOfWork.FoodItems.UpdateAsync(existingFoodItem);
        await _unitOfWork.SaveChangesAsync();

        var foodItemDto = _mapper.Map<FoodItemDto>(existingFoodItem);
        return Result<FoodItemDto>.Ok(foodItemDto);
    }

    public async Task<Result<FoodItemDto>> DeleteAsync(int id)
    {
        var existingFoodItem = await _unitOfWork.FoodItems.GetByIdAsync(id);
        if (existingFoodItem == null)
        {
            return Result<FoodItemDto>.Fail("Food item not found.");
        }

        await _unitOfWork.FoodItems.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        var foodItemDto = _mapper.Map<FoodItemDto>(existingFoodItem);
        return Result<FoodItemDto>.Ok(foodItemDto);
    }
}
