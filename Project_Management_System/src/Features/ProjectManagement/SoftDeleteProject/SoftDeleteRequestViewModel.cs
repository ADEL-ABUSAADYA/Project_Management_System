using FluentValidation;
using Project_Management_System.Features.ProjectManagement.AddProject;

namespace Project_Management_System.Features.ProjectManagement.SoftDeleteProject
{
    public record SoftDeleteRequestViewModel(Guid ProjectID);
    public class RequestEndPointModelValidator : AbstractValidator<SoftDeleteRequestViewModel>
    {
        public RequestEndPointModelValidator()
        {

        }
    }
}