// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using Project_Management_System.Common.AutoMapper;
// using Project_Management_System.Common.BaseHandlers;
// using Project_Management_System.Common.Data.Enums;
// using Project_Management_System.Common.Views;
// using Project_Management_System.Data.Repositories;
// using Project_Management_System.Features.Common.Pagination;
// using Project_Management_System.Models;
//
// namespace Project_Management_System.Features.ProjectManagement.GetAllProject.Queries
// {
//     public record GetAllProjectsQuery(int PageNumber, int PageSize) : IRequest<RequestResult<PaginatedResult<ProjectDTO>>>;
//
//     public class GetAllProjectsQueryHandler : BaseRequestHandler<GetAllProjectsQuery, RequestResult<PaginatedResult<ProjectDTO>>>
//     {
//         private readonly IRepository<Project> _repository;
//         public GetAllProjectsQueryHandler(BaseRequestHandlerParameters parameters, IRepository<Project> repository) : base(parameters)
//         {
//             _repository = repository;
//         }
//
//         public override async Task<RequestResult<PaginatedResult<ProjectDTO>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
//         {
//             var query = _repository.GetAll();
//
//             if (request.PageNumber < 1) return RequestResult<PaginatedResult<ProjectDTO>>.Failure(ErrorCode.NoProjectAdd, "there is no project add");
//
//             if (request.PageSize < 1 || request.PageSize > 100)
//                 return RequestResult<PaginatedResult<ProjectDTO>>.Failure(ErrorCode.InvalidInput, "PageSize must be between 1 and 100");
//
//             int totalProjects = await query.CountAsync(cancellationToken);
//             
//             var projects = await query
//                 .Skip((request.PageNumber - 1) * request.PageSize)
//                 .Take(request.PageSize)
//                 .ProjectTo<ProjectDTO>()
//                 .ToListAsync(cancellationToken);
//
//             if (!projects.Any())
//                 return RequestResult<PaginatedResult<ProjectDTO>>.Failure(ErrorCode.NoUsersFound, "No users found");
//
//             // Create the PaginatedResult
//             var paginatedResult = new PaginatedResult<ProjectDTO>(projects, totalProjects, request.PageNumber, request.PageSize);
//
//             return RequestResult<PaginatedResult<ProjectDTO>>.Success(paginatedResult);
//         }
//     }
// }
