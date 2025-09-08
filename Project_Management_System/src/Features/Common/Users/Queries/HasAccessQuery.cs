using MediatR;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Models;

namespace Project_Management_System.Features.Common.Users.Queries
{
    public record HasAccessQuery(Guid ID, Feature Feature) : IRequest<bool>;

    public class HasAccessQueryHandler : BaseRequestHandler<HasAccessQuery, bool>
    {
        private readonly IRepository<UserFeature> _repository;
        public HasAccessQueryHandler(BaseRequestHandlerParameters parameters, IRepository<UserFeature> repository) : base(parameters)
        {
            _repository = repository;
        }
        public override async Task<bool> Handle(HasAccessQuery request, CancellationToken cancellationToken)
        {
            var hasFeature = await _repository.AnyAsync(
                uf => uf.UserID == request.ID && uf.Feature == request.Feature);
            return hasFeature;
        }
    }
}