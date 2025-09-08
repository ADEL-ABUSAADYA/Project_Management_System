using MediatR;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.ProjectManagement.SoftDeleteProject.Queries;

namespace Project_Management_System.Features.ProjectManagement.SoftDeleteProject.Command;

public record SoftDeleteProjectOrchestrator(Guid ProjectID) : IRequest<RequestResult<bool>>;

public class SoftDeleteProjectOrchestratorHandler : BaseRequestHandler<SoftDeleteProjectCommand, RequestResult<bool>>
{
    public SoftDeleteProjectOrchestratorHandler(BaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<bool>> Handle(SoftDeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var isProjectDeleted = await _mediator.Send(new SoftDeleteProjectCommand(request.ProjectID));
        if (!isProjectDeleted.isSuccess)
        {
            return RequestResult<bool>.Failure(isProjectDeleted.errorCode, isProjectDeleted.message);
        }
        
        var sprintItemsIDs = await _mediator.Send(new GetSprintItemsIDsQuery(request.ProjectID));
        if (!isProjectDeleted.isSuccess)
            return RequestResult<bool>.Failure(isProjectDeleted.errorCode, isProjectDeleted.message);
        
        
        var isSprintItemsDeleted = await _mediator.Send(new SoftDeleteListOfSprintItemsCommand(sprintItemsIDs.data));
        if(!isSprintItemsDeleted.isSuccess)
            return RequestResult<bool>.Failure(isSprintItemsDeleted.errorCode, isSprintItemsDeleted.message);
        
        return RequestResult<bool>.Success(true);
    }
}