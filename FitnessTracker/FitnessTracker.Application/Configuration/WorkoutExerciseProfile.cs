using AutoMapper;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Configuration;

public class WorkoutExerciseProfile : Profile
{
    public WorkoutExerciseProfile()
    {
        CreateMap<WorkoutExercise, WorkoutExerciseDto>();

        CreateMap<CreateWorkoutExerciseDto, WorkoutExercise>()
            .ForMember(d => d.WorkoutId, opt => opt.Ignore());
    }
}
