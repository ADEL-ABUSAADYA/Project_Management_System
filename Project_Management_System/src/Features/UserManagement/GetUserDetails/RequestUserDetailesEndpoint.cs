using DotNetCore.CAP;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.AuthManagement.RegisterUser;
using Project_Management_System.Features.AuthManagement.RegisterUser.Commands;
using Project_Management_System.Features.UserManagement.GetAllUsers;

public class UserDetailsEndpoint : BaseEndpoint<EmptyRequestViewModel, bool>
{
    private readonly ICapPublisher _capPublisher;
    public UserDetailsEndpoint(BaseEndpointParameters<EmptyRequestViewModel> parameters, ICapPublisher capPublisher) : base(parameters)
    {
        _capPublisher = capPublisher;
    }

    [HttpPut]
    public async Task<EndpointResponse<bool>> UserDetails(EmptyRequestViewModel viewmodel)
    {
        var validationResult =  ValidateRequest(viewmodel);
        if (!validationResult.isSuccess)
            return validationResult;
      
        await _capPublisher.PublishAsync("test.message", new { Message = "Hello CAP" });

      
        return EndpointResponse<bool>.Success(true);
    }
}
