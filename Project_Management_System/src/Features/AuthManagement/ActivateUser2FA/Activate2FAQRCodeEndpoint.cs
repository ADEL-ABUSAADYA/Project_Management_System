using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtpNet;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.ActivateUser2FA.Commands;
using Project_Management_System.Features.AuthManagement.LogInUser;
using Project_Management_System.Filters;

namespace Project_Management_System.Features.AuthManagement.ActivateUser2FA;


public class Activate2FAQRCodeEndpoint : BaseEndpoint<EmptyRequestViewModel, string>
{
    public Activate2FAQRCodeEndpoint(BaseEndpointParameters<EmptyRequestViewModel> parameters) : base(parameters)
    {
    }
    
    [HttpPost]
    [Authorize]
    [TypeFilter(typeof(CustomizeAuthorizeAttribute), Arguments =new object[] {Feature.ActivateUser2FA})]
    public async Task<EndpointResponse<string>> ActivateUser2FA()
    {
        var activateCommand = await _mediator.Send(new ActivateUser2FAOrchestrator());
        if (!activateCommand.isSuccess)
            return EndpointResponse<string>.Failure(activateCommand.errorCode, activateCommand.message);
        
        return EndpointResponse<string>.Success(activateCommand.data);
    }
}