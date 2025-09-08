using MediatR;
using Project_Management_System.Common.Views;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Models;


namespace Project_Management_System.Features.AuthManagement.SendFrogetPasswordResetEmail.Queries
{
    public record GetForgetPasswordInfoQuery(string email) : IRequest<RequestResult<FrogetPasswordInfoDTO>>;


    public class ResetPsswordQueryHandler : BaseRequestHandler<GetForgetPasswordInfoQuery, RequestResult<FrogetPasswordInfoDTO>>
    {
        private readonly IRepository<User> _repository;
        public ResetPsswordQueryHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
        {
            _repository = repository;
        }

        public override async Task<RequestResult<FrogetPasswordInfoDTO>> Handle(GetForgetPasswordInfoQuery request, CancellationToken cancellationToken)
        {
            var resetInfo = await _repository.Get(U=> U.Email == request.email)
                .Select(u =>
                    new FrogetPasswordInfoDTO(
                        u.ID,
                        u.IsEmailConfirmed
                        )).FirstOrDefaultAsync();

            if (resetInfo is null )
                return RequestResult<FrogetPasswordInfoDTO>.Failure(ErrorCode.UserNotFound, "this user not found");

            if (!resetInfo.IsEmailConfirmed || resetInfo.UserID == Guid.Empty )
            {
                return RequestResult<FrogetPasswordInfoDTO>.Failure(ErrorCode.AccountNotVerified, "Verify your email address");
            }
            return RequestResult<FrogetPasswordInfoDTO>.Success(resetInfo);
            
        }
    }


}
