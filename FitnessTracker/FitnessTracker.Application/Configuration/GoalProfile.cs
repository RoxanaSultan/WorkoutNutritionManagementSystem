using AutoMapper;
using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Configuration;

public class GoalProfile : Profile
{
    public GoalProfile()
    {
        CreateMap<Goal, GoalDto>();

        CreateMap<CreateGoalDto, Goal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateGoalDto, Goal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
