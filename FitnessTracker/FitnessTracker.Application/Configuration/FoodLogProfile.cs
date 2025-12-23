using AutoMapper;
using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Configuration;

public class FoodLogProfile : Profile
{
    public FoodLogProfile()
    {
        CreateMap<FoodItem, FoodLogDto>();
        CreateMap<CreateFoodLogDto, FoodItem>();
        CreateMap<UpdateFoodLogDto, FoodItem>();
    }
}
