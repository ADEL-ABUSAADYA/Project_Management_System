using FluentValidation;
using MediatR;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Helpers;
using Project_Management_System.Models;

namespace Project_Management_System.Common.BaseHandlers
{
    public class BaseRequestHandlerParameters
    {
        private readonly IMediator _mediator;
        private readonly TokenHelper _tokenHelper;
        private readonly UserInfo _userInfo;
        private readonly CancellationToken _cancellationToken;

        public IMediator Mediator => _mediator;

        public TokenHelper TokenHelper => _tokenHelper;
        public UserInfo UserInfo => _userInfo;
        public CancellationToken CancellationToken => _cancellationToken;
        

        // Constructor accepts the generic repository type for flexibility
        public BaseRequestHandlerParameters(IMediator mediator, UserInfoProvider userInfoProvider, TokenHelper tokenHelper, CancellationTokenProvider cancellationTokenProvider)
        {
            _mediator = mediator;
            _userInfo = userInfoProvider.UserInfo;
            _tokenHelper = tokenHelper;
            _cancellationToken = cancellationTokenProvider.CancellationToken;
            
        }
    }
}
