using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Models;

namespace Project_Management_System.Features.ProjectManagement.SoftDeleteProject.Queries;

public record GetSprintItemsIDsQuery(Guid ProjectID) : IRequest<RequestResult<List<Guid>>>;

public class GetSprintItemsIDsQueryHandler : BaseRequestHandler<GetSprintItemsIDsQuery, RequestResult<List<Guid>>>
{
    private readonly IRepository<SprintItem> _repository;
    public GetSprintItemsIDsQueryHandler(BaseRequestHandlerParameters parameters, IRepository<SprintItem> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<List<Guid>>> Handle(GetSprintItemsIDsQuery request, CancellationToken cancellationToken)
    {
        var sprintItemsIDs = await _repository.Get(si => si.ProjectID == request.ProjectID && si.Deleted == false).Select(si => si.ID).ToListAsync();
        if (sprintItemsIDs.Count <= 0)
            return RequestResult<List<Guid>>.Failure(ErrorCode.NoSprintItems, "No sprint items found");
        
        return RequestResult<List<Guid>>.Success(sprintItemsIDs);
    }
}