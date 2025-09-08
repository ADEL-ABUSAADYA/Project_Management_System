﻿using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Filters;

namespace Project_Management_System.Common.BaseEndpoints;

[ApiController]
[Route("[controller]/[action]")]

// [TypeFilter(typeof(UserInfoFilter))]
public class BaseEndpoint<TRequest, TResponse> : ControllerBase
{
    protected IMediator _mediator;
    protected IValidator<TRequest> _validator;
    protected UserInfo _userInfo;

    public BaseEndpoint(BaseEndpointParameters<TRequest> parameters)
    {
        _mediator = parameters.Mediator;
        _validator = parameters.Validator;
        _userInfo = parameters.UserInfo;
    }

    protected EndpointResponse<TResponse> ValidateRequest(TRequest request)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var validationError = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));

            return EndpointResponse<TResponse>.Failure(ErrorCode.InvalidInput, validationError);
        }

        return EndpointResponse<TResponse>.Success(default);
    }
}
