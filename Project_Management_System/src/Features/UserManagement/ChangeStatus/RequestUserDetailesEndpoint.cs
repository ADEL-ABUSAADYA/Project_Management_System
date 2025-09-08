using FluentValidation;
using Project_Management_System.Features.UserManagement.GetAllUsers;

namespace Project_Management_System.Features.UserManagement.ChangeStatus
{
    public record RequestUserActivtaionStatus(Guid id);

    public class RequestUserActivtaionStatusValidator : AbstractValidator<RequestUserActivtaionStatus>
    {
        public RequestUserActivtaionStatusValidator()
        {
          
        }
    }


}