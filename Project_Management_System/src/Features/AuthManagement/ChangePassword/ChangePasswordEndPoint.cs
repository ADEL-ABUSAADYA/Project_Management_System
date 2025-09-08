using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.ChangePassword.Commands;
using Project_Management_System.Features.AuthManagement.ChangePassword;


namespace Project_Management_System.Features.AuthManagement.ChangePassword;

public class ChangePasswordEndPoint : BaseEndpoint<ChangePasswordRequestViewModel, bool>
{
    public ChangePasswordEndPoint(BaseEndpointParameters<ChangePasswordRequestViewModel> parameters) : base(parameters)
    {
    }

    [HttpPut]
    public async Task<EndpointResponse<bool>> ChangePassword(ChangePasswordRequestViewModel viewModel)
    {
        var validationResult = ValidateRequest(viewModel);
        if (!validationResult.isSuccess)
            return validationResult;

        var changePasswordCommand = new ChangePasswordCommand(viewModel.CurrentPassword, viewModel.NewPassword);
        var IsChanged = await _mediator.Send(changePasswordCommand);
        if (!IsChanged.isSuccess)
            return EndpointResponse<bool>.Failure(IsChanged.errorCode, IsChanged.message);
        return EndpointResponse<bool>.Success(true);



    }

}
