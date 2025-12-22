using AutoMapper;
using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Domain.Entities;

public class FoodItemProfile : Profile
{
    public FoodItemProfile()
    {
        CreateMap<FoodItem, FoodItemDto>();

        CreateMap<CreateFoodItemDto, FoodItem>();

        CreateMap<UpdateFoodItemDto, FoodItem>()
            .ForMember(d => d.Id, opt => opt.Ignore());
    }
}
