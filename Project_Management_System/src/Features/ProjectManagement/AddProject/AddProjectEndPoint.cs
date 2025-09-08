using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.Sig;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.RegisterUser.Commands;
using Project_Management_System.Features.ProjectManagement.AddProject.Commands;
using Project_Management_System.Filters;
using Project_Management_System.Models;

namespace Project_Management_System.Features.ProjectManagement.AddProject
{
    public class AddProjectEndPoint : BaseEndpoint<RequestAddProjectModel, bool>
    {
        private readonly ICapPublisher _capPublisher;
        public AddProjectEndPoint(BaseEndpointParameters<RequestAddProjectModel> parameters, ICapPublisher capPublisher) : base(parameters)
        {
            _capPublisher = capPublisher;
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(CustomizeAuthorizeAttribute), Arguments = new object[] { Feature.AddProject })]
        public async Task<EndpointResponse<bool>> AddProject(RequestAddProjectModel viewmodel, CancellationToken cancellationToken)
        {
            var message = new BasicMessage<RequestAddProjectModel>(viewmodel);
            await _capPublisher.PublishAsync("Adel", message);
            await _capPublisher.PublishAsync("adel", message);
            var validationResult = ValidateRequest(viewmodel);
            if (!validationResult.isSuccess)
                return validationResult;

            var Project = await _mediator.Send(new AddProjectCommand (viewmodel.Title , viewmodel.Descrbition, viewmodel.EndDate), cancellationToken);
            if (!Project.isSuccess)
                return EndpointResponse<bool>.Failure(Project.errorCode, Project.message);

            return EndpointResponse<bool>.Success(true);
        }
    }
}
