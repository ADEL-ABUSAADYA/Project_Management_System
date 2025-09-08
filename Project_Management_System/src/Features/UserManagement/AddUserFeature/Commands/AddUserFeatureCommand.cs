using MediatR;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums; 
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.Common.Users.Queries;
using Project_Management_System.Models;

namespace Project_Management_System.Features.UserManagement.AddUserFeature.Commands;

public record AddUserFeatureCommand(string Email, Feature feature) : IRequest<RequestResult<bool>>;

public class AddUserFeatureCommandHandler : BaseRequestHandler<AddUserFeatureCommand, RequestResult<bool>>
{
    private readonly IRepository<UserFeature> _repository;
    public AddUserFeatureCommandHandler(BaseRequestHandlerParameters parameters, IRepository<UserFeature> repository) :
        base(parameters)
    {
        _repository = repository;
    }

    public async override Task<RequestResult<bool>> Handle(AddUserFeatureCommand request, CancellationToken cancellationToken)
    {
        
        var userID = await _mediator.Send(new GetUserIDByEmailQuery(request.Email));
        if (!userID.isSuccess)
            return RequestResult<bool>.Failure(userID.errorCode, userID.message);

        var hasAccess = await _mediator.Send(new HasAccessQuery(userID.data, request.feature));
        if (hasAccess)
            return RequestResult<bool>.Success(true);
        
        await _repository.AddAsync(new UserFeature
            {
                Feature = request.feature,
                UserID = userID.data,
            });
        await _repository.SaveChangesAsync();

        return RequestResult<bool>.Success(true);
    }
}
