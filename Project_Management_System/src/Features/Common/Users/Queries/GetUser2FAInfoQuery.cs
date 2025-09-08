using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.Common.Users.DTOs;
using Project_Management_System.Models;


namespace Project_Management_System.Features.Common.Users.Queries;
public record GetUser2FAInfoQuery() : IRequest<RequestResult<User2FAInfoDTO>>;

public class GetUser2FAInfoQueryHandler : BaseRequestHandler<GetUser2FAInfoQuery, RequestResult<User2FAInfoDTO>>
{
    private readonly IRepository<User> _repository;
    public GetUser2FAInfoQueryHandler (BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<User2FAInfoDTO>> Handle(GetUser2FAInfoQuery request, CancellationToken cancellationToken)
    {
        var userData  = await _repository.Get(u => u.ID == _userInfo.ID)
            .Select(u => new User2FAInfoDTO(u.TwoFactorAuthEnabled, u.TwoFactorAuthsecretKey)).FirstOrDefaultAsync();
        
        if (userData is null)
        {
            return RequestResult<User2FAInfoDTO>.Failure(ErrorCode.UserNotFound, "please login again");
        }
        return RequestResult<User2FAInfoDTO>.Success(userData);
    }
}