using MediatR;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.LogInUser.Queries;
using Project_Management_System.Models;
using OtpNet;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.AuthManagement.LogInUserWith2FA.Queries;

namespace Project_Management_System.Features.AuthManagement.LogInUserWith2FA.Commands;

public record LogInUserWith2FACommand(string Email, string Otp) : IRequest<RequestResult<string>>;

public class LogInUserWith2FACommandHandler : BaseRequestHandler<LogInUserWith2FACommand, RequestResult<string>>
{
    private readonly IRepository<User> _repository;
    public LogInUserWith2FACommandHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<string>> Handle(LogInUserWith2FACommand request, CancellationToken cancellationToken)
    {
        var userInfo = await _mediator.Send(new GetUserLogInInfoWith2FAQuery(request.Email));
        if (!userInfo.isSuccess)
        {
            return RequestResult<string>.Failure(userInfo.errorCode, userInfo.message);
        }

        var user = userInfo.data;
        
        if (user.ID == Guid.Empty || !user.Is2FAEnabled || string.IsNullOrEmpty(user.TwoFactorAuthsecretKey))
        {
            return RequestResult<string>.Failure(ErrorCode.UserNotFound, "User does not exist or 2FA is not enabled.");
        }

        // Validate the OTP using the secret key stored for the user
        var isOtpValid = Validate2FACode(user.TwoFactorAuthsecretKey, request.Otp);
        if (!isOtpValid)
        {
            return RequestResult<string>.Failure(ErrorCode.Invalid2FA, "Invalid OTP.");
        }

        // OTP is valid, issue a full JWT token for authentication
        var fullAccessToken = _tokenHelper.GenerateToken(user.ID);
        return RequestResult<string>.Success(fullAccessToken);
    }

    private bool Validate2FACode(string userSecretKey, string enteredOtp)
    {
        var keyBytes = Base32Encoding.ToBytes(userSecretKey);
        var totp = new Totp(keyBytes);
        
        return totp.VerifyTotp(enteredOtp, out long timeStepMatched);
    }
}