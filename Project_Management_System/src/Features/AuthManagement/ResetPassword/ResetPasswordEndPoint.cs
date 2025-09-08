using Azure;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.ResetPassword.Commands;

namespace Project_Management_System.Features.AuthManagement.ResetPassword
{
    public class ResetPasswordEndPoint : BaseEndpoint<ResetPasswordRequestViewModel, bool>
    {
        public ResetPasswordEndPoint(BaseEndpointParameters<ResetPasswordRequestViewModel> parameters) : base(parameters)
        {
        }


        [HttpPost]
         
        public async Task<EndpointResponse<bool>> ResetPassword(ResetPasswordRequestViewModel parameters)
        {
            var validateInput = ValidateRequest(parameters);
            if (!validateInput.isSuccess) return EndpointResponse<bool>.Failure(ErrorCode.InvalidInput);

            var response = await _mediator.Send(new ResetPasswordCommand(parameters.otp, parameters.Email, parameters.Email));

            if (!response.isSuccess) return EndpointResponse<bool>.Failure(response.errorCode, response.message);

            return EndpointResponse<bool>.Success(true, "password change sucssfuly");
        }

    }
}
