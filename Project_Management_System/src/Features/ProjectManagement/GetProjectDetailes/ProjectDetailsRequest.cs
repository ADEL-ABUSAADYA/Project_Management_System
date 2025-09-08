using FluentValidation;
using Project_Management_System.Features.ProjectManagement.GetAllProject;

namespace Project_Management_System.Features.ProjectManagement.GetProjectDetailes
{
    public record ProjectDetailsRequest(int id);

    public class ProjectDetailsRequestValidator : AbstractValidator<ProjectDetailsRequest>
    {
        public ProjectDetailsRequestValidator()
        {

        }
    }


}