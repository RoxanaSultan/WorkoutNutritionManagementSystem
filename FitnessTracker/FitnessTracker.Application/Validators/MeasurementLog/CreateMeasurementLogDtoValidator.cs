using FitnessTracker.Application.DTOs.MeasurementLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.MeasurementLog;

public class CreateMeasurementLogDtoValidator : AbstractValidator<CreateMeasurementLogDto>
{
    public CreateMeasurementLogDtoValidator()
    {
        Include(new MeasurementLogValidator());

        RuleFor(x => x.UserId)
           .GreaterThan(0).WithMessage("UserId must be greater than 0.");
    }
}
