using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.AuthManagement.LogInUserWith2FA;
using Project_Management_System.Models;

namespace Project_Management_System.Features.AuthManagement.LogInUserWith2FA.Queries;
public record GetUserLogInInfoWith2FAQuery(string email) : IRequest<RequestResult<LogInInfoWith2FADTO>>;

public class GetUserLogInInfoWith2FAQueryHandler : BaseRequestHandler<GetUserLogInInfoWith2FAQuery, RequestResult<LogInInfoWith2FADTO>>
{
    private readonly IRepository<User> _repository;
    public GetUserLogInInfoWith2FAQueryHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<LogInInfoWith2FADTO>> Handle(GetUserLogInInfoWith2FAQuery request, CancellationToken cancellationToken)
    {
        var userData  = await _repository.Get(u => u.Email == request.email)
            .Select(u => new LogInInfoWith2FADTO(u.ID, u.TwoFactorAuthEnabled, u.TwoFactorAuthsecretKey)).FirstOrDefaultAsync();
        
        if (userData.ID == Guid.Empty)
        {
            return RequestResult<LogInInfoWith2FADTO>.Failure(ErrorCode.UserNotFound, "please check your email address.");
        }
        return RequestResult<LogInInfoWith2FADTO>.Success(userData);
    }
}