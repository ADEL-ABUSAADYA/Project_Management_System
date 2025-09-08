using FluentValidation;
using Project_Management_System.Features.AuthManagement.RegisterUser;

namespace Project_Management_System.Features.AuthManagement.SendFrogetPasswordResetEmail
{
    public record ForgetPasswordViewModel(string email); 


     public class ForgetPasswordViewModelValidator : AbstractValidator<ForgetPasswordViewModel>
    {
        public ForgetPasswordViewModelValidator ()
        {
         
        }
    }

}