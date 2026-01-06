using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.User;
using FluentValidation;

namespace FitnessTracker.Application.Validators.User;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ErrorMessages.UserNameRequired)
            .MaximumLength(50).WithMessage(ErrorMessages.UserNameMaxLength50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ErrorMessages.UserEmailRequired)
            .EmailAddress().WithMessage(ErrorMessages.UserEmailInvalid);

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage(ErrorMessages.UserBirthDateRequired)
            .Must(BeInThePast).WithMessage(ErrorMessages.UserBirthDateInPast)
            .Must(BeAtLeast13YearsOld).WithMessage(ErrorMessages.UserTooYoung);

        RuleFor(x => x.Height)
            .InclusiveBetween(50, 250)
            .When(x => x.Height.HasValue)
            .WithMessage(ErrorMessages.UserHeightRange);

        RuleFor(x => x.Weight)
            .InclusiveBetween(20, 400)
            .When(x => x.Weight.HasValue)
            .WithMessage(ErrorMessages.UserWeightRange);

        RuleFor(x => x.Gender)
            .Must(BeValidGender)
            .WithMessage(ErrorMessages.UserGenderInvalid);
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
