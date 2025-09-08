using FluentValidation;

namespace Project_Management_System.Features.AuthManagement.ResendRegistrationEmail;

public record ReSendRegistrationEmailRequestViewModel(string Email);

public class RegisterUserRequestViewModelValidator : AbstractValidator<ReSendRegistrationEmailRequestViewModel>
{
    public RegisterUserRequestViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please provide a valid email address.");
    }
}