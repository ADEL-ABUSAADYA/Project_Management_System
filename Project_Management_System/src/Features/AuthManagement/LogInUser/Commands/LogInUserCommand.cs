using MediatR;
using Microsoft.AspNetCore.Identity;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.Common.Users.DTOs;
using Project_Management_System.Features.AuthManagement.LogInUser.Queries;
using Project_Management_System.Models;

namespace Project_Management_System.Features.AuthManagement.LogInUser.Commands;

public record LogInUserCommand(string Email, string Password) : IRequest<RequestResult<TokenDTO>>;

public class LogInUserCommandHandler : BaseRequestHandler<LogInUserCommand, RequestResult<TokenDTO>>
{
    private readonly IRepository<User> _repository;
    public LogInUserCommandHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }
    
    public override async Task<RequestResult<TokenDTO>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        
        var userInfo = await _mediator.Send(new GetUserLogInInfoQuery(request.Email));
        if (!userInfo.isSuccess)
        {
            return RequestResult<TokenDTO>.Failure(userInfo.errorCode, userInfo.message);
        }
        var isPasswordMached = CheckPassword(request.Password, userInfo.data.hashedPassword);
        if (!isPasswordMached)
            return RequestResult<TokenDTO>.Failure(userInfo.errorCode, "password is incorrect.");
        if (!userInfo.data.IsEmailConfirmed)
        {
            return RequestResult<TokenDTO>.Failure(userInfo.errorCode, "email is not confirmed.");
        }
        if (userInfo.data.ID != Guid.Empty && !userInfo.data.Is2FAEnabled)
        {
            var token = _tokenHelper.GenerateToken(userInfo.data.ID);
            return RequestResult<TokenDTO>.Success(new TokenDTO(LogInToken: token, TokenWith2FA: ""));
        }
        if (userInfo.data.ID != Guid.Empty && userInfo.data.Is2FAEnabled)
        {
            var token2fa = _tokenHelper.Generate2FALoginToken(userInfo.data.ID);
            return RequestResult<TokenDTO>.Success(new TokenDTO(LogInToken: "", TokenWith2FA: token2fa));
        }

        return RequestResult<TokenDTO>.Failure(userInfo.errorCode, userInfo.message);
    }

    private bool CheckPassword(string requestPassword, string databasePassword)
    {
        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, databasePassword, requestPassword);
        if (passwordVerificationResult== PasswordVerificationResult.Success)
        return true;
        
        return false;
    }
}