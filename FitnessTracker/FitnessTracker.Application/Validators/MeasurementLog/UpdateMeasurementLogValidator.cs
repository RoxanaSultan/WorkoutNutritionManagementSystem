using FitnessTracker.Application.DTOs.MeasurementLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.MeasurementLog;

public class UpdateMeasurementLogValidator : AbstractValidator<UpdateMeasurementLogDto>
{
    public UpdateMeasurementLogValidator()
    {
        Include(new CreateMeasurementLogDtoValidator());
    }
}
