using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.SendFrogetPasswordResetEmail.Commands;
using Project_Management_System.src.Helpers;

namespace Project_Management_System.Features.AuthManagement.SendFrogetPasswordResetEmail
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendFrogetPasswordResstEmailEnpoint : BaseEndpoint<ForgetPasswordViewModel, bool>
    {
        public SendFrogetPasswordResstEmailEnpoint(BaseEndpointParameters<ForgetPasswordViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        public async Task<EndpointResponse<bool>> SendFrogetPasswordResstEmail(ForgetPasswordViewModel model)
        {
            var validationResult = ValidateRequest(model);
            if (!validationResult.isSuccess)
                return EndpointResponse<bool>.Failure(ErrorCode.InvalidInput);
            
            var response = await _mediator.Send(new SendForgetPasswordResetEmailCommand(model.email));

            if(!response.isSuccess)
                return EndpointResponse<bool>.Failure(response.errorCode , response.message);

            return EndpointResponse<bool>.Success(true);

        }


    }
}
