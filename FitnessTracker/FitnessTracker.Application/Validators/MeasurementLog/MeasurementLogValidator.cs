using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.MeasurementLog;
using FluentValidation;

namespace FitnessTracker.Application.Validators.MeasurementLog;

public class MeasurementLogValidator : AbstractValidator<MeasurementLogBase>
{
    public MeasurementLogValidator()
    {
        RuleFor(x => x.Date)
            .Must(d => d.ToUniversalTime() <= DateTime.UtcNow)
            .WithMessage(ErrorMessages.MeasurementLogDateNotInFuture);

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage(ErrorMessages.MeasurementLogWeightPositive);

        RuleFor(x => x.BodyFatPercentage)
            .InclusiveBetween(2, 60)
            .WithMessage(ErrorMessages.MeasurementLogBodyFatRange);

        RuleFor(x => x.Waist)
            .InclusiveBetween(10, 250)
            .WithMessage(ErrorMessages.MeasurementLogWaistRange);

        RuleFor(x => x.Chest)
            .InclusiveBetween(10, 250)
            .WithMessage(ErrorMessages.MeasurementLogChestRange);

        RuleFor(x => x.Arms)
            .InclusiveBetween(10, 250)
            .WithMessage(ErrorMessages.MeasurementLogArmsRange);
    }
}
