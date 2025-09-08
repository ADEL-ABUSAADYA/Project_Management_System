// using MediatR;
// using Project_Management_System.Common.BaseHandlers;
// using Project_Management_System.Common.Views;
//
// namespace Project_Management_System.Features.SprintItemManagement.SoftDeleteSprintItem.Commands;
//
// public record SoftDeleteSprintItemCommand(int ProjectID) : IRequest<RequestResult<bool>>;
//
// public class SoftDeleteSprintItemCommandHandler : BaseRequestHandler<SoftDeleteSprintItemCommand, RequestResult<bool>>
// {
//     public SoftDeleteSprintItemCommandHandler(BaseRequestHandlerParameters parameters) : base(parameters)
//     {
//     }
//
//     public override Task<RequestResult<bool>> Handle(SoftDeleteSprintItemCommand request, CancellationToken cancellationToken)
//     {
//         
//     }
// }