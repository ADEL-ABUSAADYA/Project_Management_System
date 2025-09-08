using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.LogInUser.Commands;
using Project_Management_System.Filters;

namespace Project_Management_System.Features.UserManagement.AddUserFeature;

public class AddUserFeatureEndpoint : BaseEndpoint<AddUserFeatureRequestViewModel, bool>
{
   public AddUserFeatureEndpoint(BaseEndpointParameters<AddUserFeatureRequestViewModel> parameters) : base(parameters)
   {
   }

   [HttpPost]
   [Authorize]
   [TypeFilter(typeof(CustomizeAuthorizeAttribute), Arguments =new object[] {Feature.AddUserFeature})]
   public async Task<EndpointResponse<bool>>  AddUserFeature(AddUserFeatureRequestViewModel viewmodel)
   {
      var validationResult =  ValidateRequest(viewmodel);
      if (!validationResult.isSuccess)
         return validationResult;
      
      var loginCommand = new LogInUserCommand(viewmodel.Email, viewmodel.Password);
      var logInToken = await _mediator.Send(loginCommand);
      if (!logInToken.isSuccess)
         return EndpointResponse<bool>.Failure(logInToken.errorCode, logInToken.message);
      
      return EndpointResponse<bool>.Success(true);
   }
}
