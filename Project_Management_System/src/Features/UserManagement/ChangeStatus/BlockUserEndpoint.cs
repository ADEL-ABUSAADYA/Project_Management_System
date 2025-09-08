
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.UserManagement.ChangeStatus.Command;


namespace Project_Management_System.Features.UserManagement.ChangeStatus
{
  
    public class BlockUserEndPoint : BaseEndpoint<RequestUserActivtaionStatus,bool >
    {
        public BlockUserEndPoint(BaseEndpointParameters<RequestUserActivtaionStatus> parameters) : base(parameters)
        {
        }

        [HttpPut]
        public async Task<EndpointResponse<bool>> GetUSerDetails([FromQuery] RequestUserActivtaionStatus request)
        {
            var validationResult = ValidateRequest(request);
            if (!validationResult.isSuccess)
                return EndpointResponse<bool>.Failure(ErrorCode.InvalidInput);
            
            var response = await _mediator.Send(new ChangeStatusCommand(request.id));

            if (!response.isSuccess)
                return EndpointResponse<bool>.Failure(response.errorCode , response.message);


            return EndpointResponse<bool>.Success(response.isSuccess, "the user has deactivate ");

        }


    }
}
