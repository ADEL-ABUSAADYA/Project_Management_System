using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Models;

namespace Project_Management_System.Features.AuthManagement.ConfirmUserRegistration.Queries;

public record IsUserRegisteredQuery(string email, string token) : IRequest<RequestResult<Guid>>;

public class IsUserRegisteredQueryHandler : BaseRequestHandler<IsUserRegisteredQuery, RequestResult<Guid>>
{
    private readonly IRepository<User> _repository;
    public IsUserRegisteredQueryHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<Guid>> Handle(IsUserRegisteredQuery request, CancellationToken cancellationToken)
    {
        var result= await _repository.Get(u => u.Email == request.email && u.ConfirmationToken == request.token).Select(u => u.ID).FirstOrDefaultAsync();
        if (result == Guid.Empty)
        {
            return RequestResult<Guid>.Failure(ErrorCode.UserNotFound);
        }
        return RequestResult<Guid>.Success(result);
    }
}