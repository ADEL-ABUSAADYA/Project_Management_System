using MediatR;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Models;

namespace Project_Management_System.Features.ProjectManagement.SoftDeleteProject.Command;

public record SoftDeleteListOfSprintItemsCommand(List<Guid> SprintIDs) : IRequest<RequestResult<bool>>;

public class SoftDeleteListOfSprintItemsCommandHandler : BaseRequestHandler<SoftDeleteListOfSprintItemsCommand, RequestResult<bool>>
{
    private readonly IRepository<SprintItem> _repository;
    public SoftDeleteListOfSprintItemsCommandHandler(BaseRequestHandlerParameters parameters, IRepository<SprintItem> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<bool>> Handle(SoftDeleteListOfSprintItemsCommand request,
        CancellationToken cancellationToken)
    {
        List<SprintItem> sprintItems = new List<SprintItem>();
        foreach (var itemID in request.SprintIDs)
        {
            sprintItems.Add(
                new SprintItem
                {
                    ID = itemID,
                    Deleted = true,
                    UpdatedBy = _userInfo.ID
                }
            );
        }

        foreach (var sprintItem in sprintItems)
        {
            await _repository.SaveIncludeAsync(sprintItem, nameof(SprintItem.Deleted), nameof(SprintItem.UpdatedBy));
        }

        await _repository.SaveChangesAsync();

        return RequestResult<bool>.Success(true);
    }
}