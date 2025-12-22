using FitnessTracker.Application.DTOs.User;
using FluentValidation;

namespace FitnessTracker.Application.Validators.User;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(50).WithMessage("Name must have maximum 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email must be in a valid format.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate cannot be empty.")
            .Must(BeInThePast).WithMessage("BirthDate must be in the past.")
            .Must(BeAtLeast13YearsOld).WithMessage("User must be at least 13 years old.");

        RuleFor(x => x.Height)
            .InclusiveBetween(50, 250)
            .When(x => x.Height.HasValue)
            .WithMessage("Height must be between 50 cm and 250 cm.");

        RuleFor(x => x.Weight)
            .InclusiveBetween(20, 400)
            .When(x => x.Weight.HasValue)
            .WithMessage("Weight must be between 20 kg and 400 kg.");

        RuleFor(x => x.Gender)
            .Must(BeValidGender)
            .WithMessage("Gender must be either 'male' or 'female'.");
    }

    private bool BeInThePast(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return birthDate < today;
    }

    private bool BeAtLeast13YearsOld(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age >= 13;
    }

    private bool BeValidGender(string? gender)
    {
        if (string.IsNullOrEmpty(gender))
        {
            return true;
        }

        gender = gender.Trim().ToLowerInvariant();
        return gender == "male" || gender == "female";
    }
}
