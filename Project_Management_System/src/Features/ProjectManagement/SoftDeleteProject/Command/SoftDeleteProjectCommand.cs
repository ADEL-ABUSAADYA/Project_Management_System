using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Models;


namespace Project_Management_System.Features.ProjectManagement.SoftDeleteProject.Command
{
    public record SoftDeleteProjectCommand(Guid ProjectID) : IRequest<RequestResult<bool>>;

    public class DeleteProjectCommandHandler : BaseRequestHandler<SoftDeleteProjectCommand, RequestResult<bool>>
    {
        private readonly IRepository<Project> _repository;
        public DeleteProjectCommandHandler(BaseRequestHandlerParameters parameters, IRepository<Project> repository) : base(parameters)
        {
            _repository = repository;
        }

        public override async Task<RequestResult<bool>> Handle(SoftDeleteProjectCommand request, CancellationToken cancellationToken)
        {
            // var project = await _repository.Get(c => c.ID == softDeleteRequest.ProjectId && !c.Deleted).FirstOrDefaultAsync(); 

          

            //foreach (var item in project.SprintItems)
            //{

            //    item.Deleted = true;

            //}

            
            // //await _repository.SaveIncludeAsync(project , nameof(project.SprintItems));
            // await _repository.Delete(project);
            // await _repository.SaveChangesAsync();
            return RequestResult<bool>.Success(true, "project deleted succssfully ");
        }
    }

}
