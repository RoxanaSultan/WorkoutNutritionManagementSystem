using AutoMapper;
using FitnessTracker.Application.DTOs.User;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Application.Services.Validators;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserServiceValidator _userServiceValidator;

    public UserService(IUserRepository userRepository, IMapper mapper, IUserServiceValidator userServiceValidator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userServiceValidator = userServiceValidator;
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

        return Result<IEnumerable<UserDto>>.Ok(usersDto);
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return Result<UserDto>.Fail("User not found.");

        var userDto = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Ok(userDto);
    }

    public async Task<Result<UserDto>> CreateAsync(CreateUserDto createUserDto)
    {
        var validationResult = await _userServiceValidator.ValidateCreateUserAsync(createUserDto.Email);

        if (!validationResult.Success)
        {
            return Result<UserDto>.Fail(validationResult.Error!);
        }

        var user = _mapper.Map<User>(createUserDto);
        await _userRepository.CreateAsync(user);

        var userDto = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Ok(userDto);
    }

    public async Task<Result<UserDto>> UpdateAsync(int id, UpdateUserDto updateUserDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);

        if (existingUser == null)
        {
            return Result<UserDto>.Fail("User not found.");
        }

        var validationResult = await _userServiceValidator.ValidateUpdateUserAsync(id, updateUserDto.Email);

        if (!validationResult.Success)
        {
            return Result<UserDto>.Fail(validationResult.Error!);
        }

        _mapper.Map(updateUserDto, existingUser);
        await _userRepository.UpdateAsync(existingUser);

        var userDto = _mapper.Map<UserDto>(existingUser);
        return Result<UserDto>.Ok(userDto);
    }

    public async Task<Result<UserDto>> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return Result<UserDto>.Fail("User not found.");

        await _userRepository.DeleteAsync(id);

        var userDto = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Ok(userDto);
    }
}