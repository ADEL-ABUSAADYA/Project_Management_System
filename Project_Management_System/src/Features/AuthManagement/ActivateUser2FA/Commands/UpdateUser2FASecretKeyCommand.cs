using MediatR;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.Common.Users.Queries;
using Project_Management_System.Models;


public record UpdateUser2FASecretKeyCommand(string User2FASecretKey) : IRequest<RequestResult<bool>>;

public class UpdateUser2FASecretKeyCommandHandler : BaseRequestHandler<UpdateUser2FASecretKeyCommand, RequestResult<bool>>
{
    private readonly IRepository<User> _repository;
    public UpdateUser2FASecretKeyCommandHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<bool>> Handle(UpdateUser2FASecretKeyCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            ID = _userInfo.ID,
            TwoFactorAuthsecretKey = request.User2FASecretKey,
            TwoFactorAuthEnabled = true,
        };

        try
        {

            await _repository.SaveIncludeAsync(user, nameof(user.TwoFactorAuthsecretKey), nameof(user.TwoFactorAuthEnabled));
            await _repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError, e.Message);
        }

        return RequestResult<bool>.Success(true);
    }
}