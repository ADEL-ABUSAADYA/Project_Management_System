using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.UserManagement.GetAllUsers.Queries;
using Project_Management_System.Filters;

namespace Project_Management_System.Features.UserManagement.GetAllUsers;

public class GetAllUsersEndpoint : BaseEndpoint<PaginationRequestViewModel ,UserResponseViewModel>
{
   public GetAllUsersEndpoint(BaseEndpointParameters<PaginationRequestViewModel> parameters) : base(parameters)
   {
   }

    [HttpGet]
    [Authorize]
    [TypeFilter(typeof(CustomizeAuthorizeAttribute), Arguments =new object[] {Feature.AddProject})]
    public async Task<EndpointResponse<UserResponseViewModel>> GetAllUsers([FromQuery] PaginationRequestViewModel paginationRequest)
   {

        var validationResult = ValidateRequest(paginationRequest);
        if (!validationResult.isSuccess)
            return validationResult;

        var allUsers = await _mediator.Send(new GetAllUsersQuery(paginationRequest.PageNumber, paginationRequest.PageSize));

      if (!allUsers.isSuccess)
         return EndpointResponse<UserResponseViewModel>.Failure(allUsers.errorCode, allUsers.message);

      var paginatedResult = allUsers.data;

      var response = new UserResponseViewModel
      {
         Users = paginatedResult.Items.Select(u => new UserDTO
         {
            Email = u.Email,
            Name = u.Name,
            PhoneNo = u.PhoneNo,
            IsActive = u.IsActive
         }).ToList(),
         TotalCount = paginatedResult.TotalCount,
         PageNumber = paginatedResult.PageNumber,
         PageSize = paginatedResult.PageSize
      };

      return EndpointResponse<UserResponseViewModel>.Success(response);
   }
}
