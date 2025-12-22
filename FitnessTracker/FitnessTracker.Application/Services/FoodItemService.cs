using AutoMapper;
using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Application.Services.Validators;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class FoodItemService : IFoodItemService
{
    private readonly IFoodItemRepository _foodItemRepository;
    private readonly IMapper _mapper;
    private readonly IFoodItemServiceValidator _validator;

    public FoodItemService(
        IFoodItemRepository foodItemRepository,
        IMapper mapper,
        IFoodItemServiceValidator validator)
    {
        _foodItemRepository = foodItemRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<FoodItemDto>>> GetAllAsync()
    {
        var items = await _foodItemRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<FoodItemDto>>(items);
        return Result<IEnumerable<FoodItemDto>>.Ok(dtos);
    }

    public async Task<Result<FoodItemDto>> GetByIdAsync(int id)
    {
        var item = await _foodItemRepository.GetByIdAsync(id);
        if (item == null)
            return Result<FoodItemDto>.Fail("Food item not found.");

        return Result<FoodItemDto>.Ok(_mapper.Map<FoodItemDto>(item));
    }

    public async Task<Result<FoodItemDto>> CreateAsync(CreateFoodItemDto createDto)
    {
        var validation = await _validator.ValidateCreateAsync(createDto);
        if (!validation.Success)
            return Result<FoodItemDto>.Fail(validation.Error);

        // normalize name înainte de persist (ca să păstrezi consistența)
        createDto.Name = createDto.Name.Trim();

        var entity = _mapper.Map<FoodItem>(createDto);
        await _foodItemRepository.CreateAsync(entity);

        return Result<FoodItemDto>.Ok(_mapper.Map<FoodItemDto>(entity));
    }

    public async Task<Result<FoodItemDto>> UpdateAsync(int id, UpdateFoodItemDto updateDto)
    {
        var existing = await _foodItemRepository.GetByIdAsync(id);
        if (existing == null)
            return Result<FoodItemDto>.Fail("Food item not found.");

        var validation = await _validator.ValidateUpdateAsync(updateDto, id);
        if (!validation.Success)
            return Result<FoodItemDto>.Fail(validation.Error);

        updateDto.Name = updateDto.Name.Trim();

        _mapper.Map(updateDto, existing);
        await _foodItemRepository.UpdateAsync(existing);

        return Result<FoodItemDto>.Ok(_mapper.Map<FoodItemDto>(existing));
    }

    public async Task<Result<FoodItemDto>> DeleteAsync(int id)
    {
        var existing = await _foodItemRepository.GetByIdAsync(id);
        if (existing == null)
            return Result<FoodItemDto>.Fail("Food item not found.");

        await _foodItemRepository.DeleteAsync(id);
        return Result<FoodItemDto>.Ok(_mapper.Map<FoodItemDto>(existing));
    }
}
