using System.Text.Json;
using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.AuthManagement.RegisterUser.Queries;
using Project_Management_System.Models;

namespace Project_Management_System.Features.AuthManagement.RegisterUser.Commands;

public record RegisterUserCommand(string email, string password, string name, string phoneNo, string country) : IRequest<RequestResult<bool>>;

public class RegisterUserCommandHandler : BaseRequestHandler<RegisterUserCommand, RequestResult<bool>>
{
    private readonly IRepository<User> _repository;
    private readonly ICapPublisher _capPublisher;

    public RegisterUserCommandHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository,
        ICapPublisher capPublisher) : base(parameters)
    {
        _repository = repository;
        _capPublisher = capPublisher;
    }

    public async override Task<RequestResult<bool>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var reponse = await _mediator.Send(new IsUserExistQuery(request.email));
        if (reponse.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.UserAlreadyExist);

        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        var password = passwordHasher.HashPassword(null, request.password);

        var user = new User
        {
            Email = request.email,
            Password = password,
            Name = request.name,
            PhoneNo = request.phoneNo,
            Country = request.country,
            RoleID = new Guid("80146a4c-2dbe-4eb7-b4dd-ba1d3e8eeb93"),
            IsActive = true,
            ConfirmationToken = Guid.NewGuid().ToString()
        };


        var userID = await _repository.AddAsync(user, _cancellationToken);
        await _repository.SaveChangesAsync(_cancellationToken);

        if (userID == Guid.Empty)
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError);

        var message = new UserRegisteredEvent(user.Email, user.Name, $"{user.ConfirmationToken}", DateTime.UtcNow);
        var messageJson = JsonSerializer.Serialize(message);
        await _capPublisher.PublishAsync("user.registered", messageJson);

        
        return RequestResult<bool>.Success(true);
    }
}