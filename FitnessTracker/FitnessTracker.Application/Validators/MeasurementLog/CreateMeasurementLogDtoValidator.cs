using FitnessTracker.Application.DTOs.MeasurementLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.MeasurementLog;

public class CreateMeasurementLogDtoValidator : AbstractValidator<CreateMeasurementLogDto>
{
    public CreateMeasurementLogDtoValidator()
    {
        RuleFor(x => x.Date)
            .Must(d => d.ToUniversalTime() <= DateTime.UtcNow).WithMessage("Date can't be in the future.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        RuleFor(x => x.BodyFatPercentage)
            .InclusiveBetween(2, 60).WithMessage("BodyFatPercentage must be between 2 and 60%.");

        RuleFor(x => x.Waist)
            .InclusiveBetween(10, 250).WithMessage("Waist circumference must be within a realistic range.");

        RuleFor(x => x.Chest)
            .InclusiveBetween(10, 250).WithMessage("Chest circumference must be within a realistic range.");

        RuleFor(x => x.Arms)
            .InclusiveBetween(10, 250).WithMessage("Arm circumference must be within a realistic range.");
    }
}
