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

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
            .ForMember(dest => dest.FoodLogs, opt => opt.Ignore())
            .ForMember(dest => dest.Goals, opt => opt.Ignore())
            .ForMember(dest => dest.MeasurementLogs, opt => opt.Ignore())
            .ForMember(dest => dest.Workouts, opt => opt.Ignore());
    }
}
