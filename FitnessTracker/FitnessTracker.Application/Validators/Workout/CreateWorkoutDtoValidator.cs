using FluentValidation;
using FitnessTracker.Application.DTOs.Workout;

namespace FitnessTracker.Application.Validators.Workout;

public class CreateWorkoutDtoValidator : AbstractValidator<CreateWorkoutDto>
{
    public CreateWorkoutDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be a valid value.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Workout date is required.")
            .Must(NotBeInFuture)
            .WithMessage("Workout date cannot be in the future.");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0)
            .WithMessage("Duration must be a positive number.")
            .InclusiveBetween(5, 300)
            .WithMessage("Duration must be between 5 and 300 minutes.");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("Notes cannot exceed 500 characters.");
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