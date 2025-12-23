using AutoMapper;
using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Configuration;

public class GoalProfile : Profile
{
    public GoalProfile()
    {
        CreateMap<Goal, GoalDto>();
        CreateMap<CreateGoalDto, Goal>();
        CreateMap<UpdateGoalDto, Goal>();
    }
}
