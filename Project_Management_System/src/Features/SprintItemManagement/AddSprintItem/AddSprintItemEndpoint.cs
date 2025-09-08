using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.Views;
using Project_Management_System.Features.SprintItemManagement.AddSprintItem.Command;

namespace Project_Management_System.Features.SprintItemManagement.AddSprintItem
{
    
    public class AddSprintItemEndpoint : BaseEndpoint<AddSprintItemRequestViewModel, bool>
    {

        public AddSprintItemEndpoint(BaseEndpointParameters<AddSprintItemRequestViewModel> parameters) : base(parameters)
        {
        }
        
        [HttpPost]
        public async Task<EndpointResponse<bool>> AddTask(AddSprintItemRequestViewModel viewmodel)
        {
            var validationResult = ValidateRequest(viewmodel);
            if (!validationResult.isSuccess)
                return validationResult;

            var task = await _mediator.Send(new AddSprintItemCommand(viewmodel.Title,viewmodel.Description));
            if (!task.isSuccess)
                return EndpointResponse<bool>.Failure(task.errorCode, task.message);

            return EndpointResponse<bool>.Success(true);
        }
    }
}
