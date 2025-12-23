using AutoMapper;
using FitnessTracker.Application.DTOs.MeasurementLog;
using FitnessTracker.Domain.Entities;

public class MeasurementLogProfile : Profile
{
    public MeasurementLogProfile()
    {
        CreateMap<MeasurementLog, MeasurementLogDto>();
        CreateMap<CreateMeasurementLogDto, MeasurementLog>();
        CreateMap<UpdateMeasurementLogDto, MeasurementLog>();
    }
}