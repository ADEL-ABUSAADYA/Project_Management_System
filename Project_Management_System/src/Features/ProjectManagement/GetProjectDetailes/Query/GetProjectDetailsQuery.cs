// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using Project_Management_System.Common.BaseHandlers;
// using Project_Management_System.Common.Data.Enums;
// using Project_Management_System.Common.Helper;
// using Project_Management_System.Common.Views;
// using Project_Management_System.Features.Common.Pagination;
// using Project_Management_System.Features.UserManagement.GetAllUsers;
// using Project_Management_System.Models;
//
// namespace Project_Management_System.Features.ProjectManagement.GetProjectDetailes.Queries
// {
//     public record GetProjectDetailsQuery(int id) : IRequest<RequestResult<GetProjectRequestDTO>>;
//
//     public class GetProjectDetailsQueryHandler : BaseRequestHandler<GetProjectDetailsQuery, RequestResult<GetProjectRequestDTO>, Project>
//     {
//         public GetProjectDetailsQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<Project> parameters) : base(parameters)
//         {
//         }
//
//         public override async Task<RequestResult<GetProjectRequestDTO>> Handle(GetProjectDetailsQuery softDeleteRequest, CancellationToken cancellationToken)
//         {
//             var query = await _repository.Get(c => c.ID == softDeleteRequest.id && !c.Deleted).Select(c => new
//             {
//                 c.SprintItems,
//                 c.CreatedDate,
//                 c.Description,
//                 c.Title
//             }).FirstOrDefaultAsync();
//
//             if (query == null) return RequestResult<GetProjectRequestDTO>.Failure(ErrorCode.ProjectNotFound, "there is no project with this id");
//
//             var project = new GetProjectRequestDTO
//             {
//                 title = query.Title,
//                 description = query.Description,
//                 NumTask = query.SprintItems.Select(c => c.ID).Count(),
//                 NumUSers = query.SprintItems.Select(c => c.UserID).Distinct().Count(),
//                 CreatedDate = query.CreatedDate,
//             };
//
//             return RequestResult<GetProjectRequestDTO>.Success(project);
//         }
//     }
// }
