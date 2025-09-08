using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.Common.Users.DTOs;
using Project_Management_System.Features.AuthManagement.LogInUser;
using Project_Management_System.Models;

namespace Project_Management_System.Features.AuthManagement.ChangePassword.Queries;

public record GetPasswordByIDQuery () : IRequest<RequestResult<string>>;

public class GetPasswordByIDQueryHandler : BaseRequestHandler<GetPasswordByIDQuery, RequestResult<string>>
{
    private readonly IRepository<User> _repository;
    public GetPasswordByIDQueryHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<string>> Handle(GetPasswordByIDQuery request, CancellationToken cancellationToken)
    {
        var password = await _repository.Get(u => u.ID == _userInfo.ID).Select(u => u.Password).FirstOrDefaultAsync();
        
        if (string.IsNullOrWhiteSpace(password))
            return RequestResult<string>.Failure(ErrorCode.PasswordTokenNotMatch, "Password Token Not Match");

        return RequestResult<string>.Success(password);


    }
}

