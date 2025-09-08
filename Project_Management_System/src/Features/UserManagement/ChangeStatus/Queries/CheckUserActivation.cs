using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.Common.Pagination;
using Project_Management_System.Models;

namespace Project_Management_System.Features.UserManagement.ChangeStatus.Queries;

public record CheckUserActivation(int PageNumber , int PageSize) : IRequest<RequestResult<bool>>;

public class CheckUserActivationHandler : BaseRequestHandler<CheckUserActivation, RequestResult<bool>>
{
    private readonly IRepository<User> _repository;
    public CheckUserActivationHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
    {
        _repository = repository;
    }

    public override Task<RequestResult<bool>> Handle(CheckUserActivation request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}