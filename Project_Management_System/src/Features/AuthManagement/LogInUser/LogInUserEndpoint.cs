using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.LogInUser.Commands;
using Project_Management_System.Features.AuthManagement.RegisterUser.Commands;
using Project_Management_System.Features.Common.Users.DTOs;


namespace Project_Management_System.Features.AuthManagement.LogInUser;

public class LogInUserEndpoint : BaseEndpoint<LogInUserRequestViewModel, LoginResponeViewModel>
{
   public LogInUserEndpoint(BaseEndpointParameters<LogInUserRequestViewModel> parameters) : base(parameters)
   {
   }

   [HttpPost]
   public async Task<EndpointResponse<LoginResponeViewModel>> LogInUser(LogInUserRequestViewModel viewmodel)
   {
        BackgroundJob.Enqueue(() => Console.WriteLine("gfdgdfgdfg"));
        var validationResult =  ValidateRequest(viewmodel);
      if (!validationResult.isSuccess)
         return validationResult;

      
      var logInToken = await _mediator.Send(new LogInUserCommand(viewmodel.Email, viewmodel.Password));
      if (!logInToken.isSuccess)
         return EndpointResponse<LoginResponeViewModel>.Failure(logInToken.errorCode, logInToken.message);
      
      return EndpointResponse<LoginResponeViewModel>.Success(new LoginResponeViewModel(Token: logInToken.data.LogInToken, TokenWith2FA: logInToken.data.TokenWith2FA));
   }
}
