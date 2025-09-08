using FluentValidation;
using Project_Management_System.Features.AuthManagement.RegisterUser;
using Project_Management_System.Features.AuthManagement.RegisterUser.Commands;

namespace Project_Management_System.Features.ProjectManagement.AddProject
{
    public class RequestAddProjectModel
    {
        public string Title { get;  set; }
        public string Descrbition { get;  set; }
        
        public DateTime EndDate { get;  set; }
    }

    public class RequestAddProjectModelValidator : AbstractValidator<RequestAddProjectModel>
    {
        public RequestAddProjectModelValidator()
        {
  
        }
    }
}