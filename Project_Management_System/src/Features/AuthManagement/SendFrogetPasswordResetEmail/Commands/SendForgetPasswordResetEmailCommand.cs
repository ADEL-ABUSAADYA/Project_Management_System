using Hangfire;
using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.AuthManagement.ReSendRegistrationEmail.Queries;
using Project_Management_System.Features.AuthManagement.SendFrogetPasswordResetEmail.Queries;
using Project_Management_System.Models;
using Project_Management_System.src.Helpers;


namespace Project_Management_System.Features.AuthManagement.SendFrogetPasswordResetEmail.Commands;

public record SendForgetPasswordResetEmailCommand(string email) : IRequest<RequestResult<bool>>;

public class SendForgetPasswordResetEmailCommandHandler : BaseRequestHandler<SendForgetPasswordResetEmailCommand, RequestResult<bool>>
{
    private readonly IRepository<User> _repository;
    public SendForgetPasswordResetEmailCommandHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override async Task<RequestResult<bool>> Handle(SendForgetPasswordResetEmailCommand request,
        CancellationToken cancellationToken)
    {
        var passwordResetData = await _mediator.Send(new GetForgetPasswordInfoQuery(request.email));

        if (!passwordResetData.isSuccess)
            return RequestResult<bool>.Failure(passwordResetData.errorCode, passwordResetData.message);
        
        var passwordResetCode =Guid.NewGuid().ToString().Substring(0, 6);

        var user = new User
        {
            ID = passwordResetData.data.UserID,
            ResetPasswordToken = passwordResetCode
        };

        await _repository.SaveIncludeAsync(user, nameof(User.ResetPasswordToken)); 
             
        await _repository.SaveChangesAsync();

      

        BackgroundJob.Enqueue(() => EmailHelper.SendEmail(user.Email, user.ResetPasswordToken, null));
        return RequestResult<bool>.Success(true);
    }
    
   }

