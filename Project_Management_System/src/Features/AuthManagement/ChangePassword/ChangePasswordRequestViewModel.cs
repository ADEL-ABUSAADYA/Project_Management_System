﻿using FluentValidation;

namespace Project_Management_System.Features.AuthManagement.ChangePassword;

public record ChangePasswordRequestViewModel(
       string CurrentPassword,
       string NewPassword);
public class ChangePasswordRequestViewModelValidator : AbstractValidator<ChangePasswordRequestViewModel>
{
    public ChangePasswordRequestViewModelValidator()
    {
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
        RuleFor(x => x.CurrentPassword).NotEmpty();
    }
}
