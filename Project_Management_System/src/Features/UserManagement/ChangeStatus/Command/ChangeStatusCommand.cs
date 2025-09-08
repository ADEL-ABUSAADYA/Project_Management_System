using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Models;

namespace Project_Management_System.Features.UserManagement.ChangeStatus.Command
{
    public record ChangeStatusCommand(Guid id) : IRequest<RequestResult<bool>>;

    public class BlockUserCommandHandler : BaseRequestHandler<ChangeStatusCommand, RequestResult<bool>>
    {
        private readonly IRepository<User> _repository;
        public BlockUserCommandHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
        {
            _repository = repository;
        }

        public override async Task<RequestResult<bool>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            var checkActivtion = await  _repository 
               .Get(c => c.ID == request.id)
               .Select(c=> new  { ID = c.ID ,  IsActive =c.IsActive })
               .FirstOrDefaultAsync();

            if (checkActivtion == null) return RequestResult<bool>.Failure(ErrorCode.NoUsersFound, "this user not found");

         

            var changeStatus = !checkActivtion.IsActive;   
                 

            var user = new User { ID = checkActivtion.ID  , IsActive = changeStatus };


          await  _repository.SaveIncludeAsync(user , nameof(user.IsActive));

          await  _repository.SaveChangesAsync();

         return RequestResult<bool>.Success(true);  
        }
    }



}
