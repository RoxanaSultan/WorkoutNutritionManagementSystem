using AutoMapper;
using FitnessTracker.Application.DTOs.User;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Configuration;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
