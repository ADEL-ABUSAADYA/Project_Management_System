using FluentValidation;

namespace Project_Management_System.Common.Views;

public record EmptyRequestViewModel();

public class LogInUserRequestViewModelValidator : AbstractValidator<EmptyRequestViewModel>
{
    public LogInUserRequestViewModelValidator()
    {
        
    }
}