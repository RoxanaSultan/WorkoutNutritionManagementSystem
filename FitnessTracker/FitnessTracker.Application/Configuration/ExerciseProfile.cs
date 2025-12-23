using AutoMapper;
using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Configuration;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<CreateExerciseDto, Exercise>();
        CreateMap<UpdateExerciseDto, Exercise>();
    }
}
