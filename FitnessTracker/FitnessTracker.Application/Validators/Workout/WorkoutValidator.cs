using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.Workout;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Workout;

public class WorkoutValidator : AbstractValidator<WorkoutBase>
{
    public WorkoutValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage(ErrorMessages.WorkoutDateRequired)
            .Must(NotBeInFuture)
            .WithMessage(ErrorMessages.WorkoutDateNotInFuture);

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.WorkoutDurationPositive)
            .InclusiveBetween(5, 300)
            .WithMessage(ErrorMessages.WorkoutDurationRange);

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage(ErrorMessages.WorkoutNotesMaxLength500);
    }

    private bool NotBeInFuture(DateTime date)
    {
        var dateToMinute = new DateTime(
            date.Year,
            date.Month,
            date.Day,
            date.Hour,
            date.Minute,
            0);

        return dateToMinute <= DateTime.Now;
    }
}
