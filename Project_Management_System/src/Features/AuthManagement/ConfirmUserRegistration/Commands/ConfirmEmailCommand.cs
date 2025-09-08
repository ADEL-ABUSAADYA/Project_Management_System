using MediatR;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.AuthManagement.ConfirmUserRegistration.Queries;
using Project_Management_System.Models;

namespace Project_Management_System.Features.AuthManagement.ConfirmUserRegistration.Commands;

public record ConfirmEmailCommand(string email, string token) : IRequest<RequestResult<bool>>;

public class ConfirmEmailHandler : BaseRequestHandler<ConfirmEmailCommand, RequestResult<bool>>
{
    private readonly IRepository<User> _repository;
    public ConfirmEmailHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<bool>> Handle(ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
        var isRegistered = await _mediator.Send(new IsUserRegisteredQuery(request.email, request.token));

        User user = new User();
        if (isRegistered.isSuccess)
        {
            user.ID = isRegistered.data;
            user.IsEmailConfirmed = true;
            user.ConfirmationToken = null;

            var result = await _repository.SaveIncludeAsync(user, nameof(User.IsEmailConfirmed),
                nameof(User.ConfirmationToken));

            await _repository.SaveChangesAsync();
            return RequestResult<bool>.Success(result);
        }

        return RequestResult<bool>.Failure(isRegistered.errorCode, isRegistered.message);
    }

    
}



