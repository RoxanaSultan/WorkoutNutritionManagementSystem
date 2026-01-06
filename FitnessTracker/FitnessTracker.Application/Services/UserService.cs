using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.User;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserServiceValidator _userServiceValidator;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IUserServiceValidator userServiceValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userServiceValidator = userServiceValidator;
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
        
        return Result<IEnumerable<UserDto>>.Ok(userDtos);
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user == null)
        {
            return Result<UserDto>.Fail(ErrorMessages.UserNotFound);
        }

        var userDto = _mapper.Map<UserDto>(user);

        return Result<UserDto>.Ok(userDto);
    }

    public async Task<Result<UserDto>> CreateAsync(CreateUserDto createUserDto)
    {
        var validation = await _userServiceValidator.ValidateCreateUserAsync(createUserDto.Email);

        if (!validation.Success)
        {
            return Result<UserDto>.Fail(validation.Error!);
        }

        var user = _mapper.Map<User>(createUserDto);
        await _unitOfWork.Users.CreateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return Result<UserDto>.Ok(userDto);
    }

    public async Task<Result<UserDto>> UpdateAsync(int id, UpdateUserDto updateUserDto)
    {
        var existingUser = await _unitOfWork.Users.GetByIdAsync(id);

        if (existingUser == null)
        {
            return Result<UserDto>.Fail(ErrorMessages.UserNotFound);
        }

        var validationResult = await _userServiceValidator.ValidateUpdateUserAsync(id, updateUserDto.Email);

        if (!validationResult.Success)
        {
            return Result<UserDto>.Fail(validationResult.Error!);
        }

        _mapper.Map(updateUserDto, existingUser);

        await _unitOfWork.Users.UpdateAsync(existingUser);
        await _unitOfWork.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(existingUser);

        return Result<UserDto>.Ok(userDto);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var user = await _unitOfWork.Users.ExistsAsync(id);

        if (!user)
        {
            return Result.Fail(ErrorMessages.UserNotFound);
        }

        var workoutIds = await _unitOfWork.Workouts.GetWorkoutIdsByUserIdAsync(id);
        if (workoutIds.Count > 0)
        {
            await _unitOfWork.Workouts.DeleteByWorkoutIdsAsync(workoutIds);
        }

        await _unitOfWork.Workouts.DeleteByUserIdAsync(id);
        await _unitOfWork.FoodLogs.DeleteByUserIdAsync(id);
        await _unitOfWork.Goals.DeleteByUserIdAsync(id);
        await _unitOfWork.MeasurementLogs.DeleteByUserIdAsync(id);

        await _unitOfWork.Users.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}