using AutoMapper;
using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Configuration;

public class WorkoutProfile : Profile
{
    public WorkoutProfile()
    {
        CreateMap<Workout, WorkoutDto>();

        CreateMap<CreateWorkoutDto, Workout>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateWorkoutDto, Workout>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
