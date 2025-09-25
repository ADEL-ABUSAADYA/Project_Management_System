using Autofac;
using DotNetCore.CAP;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_Management_System.Common;
using Project_Management_System.Common.BaseEndpoints;
using Project_Management_System.Common.BaseHandlers;
using Project_Management_System.Common.Views;
using Project_Management_System.Data;
using Project_Management_System.Data.Interceptors;
using Project_Management_System.Data.Repositories;
using Project_Management_System.Features.AuthManagement.ActivateUser2FA;
using Project_Management_System.Features.AuthManagement.ConfirmUserRegistration;
using Project_Management_System.Features.AuthManagement.LogInUser;
using Project_Management_System.Features.AuthManagement.RegisterUser;
using Project_Management_System.Features.AuthManagement.RegisterUser.Consumers;
using Project_Management_System.Features.Common.Pagination;
using Project_Management_System.Features.UserManagement.GetAllUsers;
using Project_Management_System.Features.UserManagement.GetAllUsers.Queries;
using Project_Management_System.Filters;
using Project_Management_System.Helpers;
using Project_Management_System.Middlewares;
using Project_Management_System.Models;
using System.Reflection;
using System.Text;
using Module = Autofac.Module;

namespace Project_Management_System.Configrations
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Database Context Registration
            builder.Register(context =>
            {
                var config = context.Resolve<IConfiguration>();
                var connectionString = config.GetConnectionString("DefaultConnection");
                var options = new DbContextOptionsBuilder<Context>()
                    .UseSqlServer(connectionString)
                    .Options;
            
                return new Context(options);
            }).As<Context>().InstancePerLifetimeScope();
            #endregion

            #region Services Registration
            builder.RegisterType<UserInfo>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TokenHelper>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoProvider>().AsSelf().InstancePerLifetimeScope();
            #endregion

            #region MediatR Handlers Registration
            // Register MediatR softDeleteRequest handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            #endregion

            #region FluentValidation Registration
            // Register FluentValidation validators
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            #endregion

            #region Endpoint Registration
            // Register specific endpoint parameters
            builder.RegisterGeneric(typeof(BaseEndpointParameters<>))
                .AsSelf()
                .InstancePerLifetimeScope();
            // Register endpoints
            builder.RegisterType<RegisterUserEndpoint>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ConfirmEmailEndpoint>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<Activate2FAQRCodeEndpoint>().AsSelf().InstancePerLifetimeScope();
            #endregion

            #region Repository Registration
            // Register repositories
            // builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<Repository<BaseModel>>().As<IRepository<BaseModel>>().InstancePerLifetimeScope();
            #endregion

            #region JWT Authentication Registration
            // Register JWT authentication
            builder.Register(context =>
            {
                var config = context.Resolve<IConfiguration>();
                var jwtSettings = config.GetSection("JwtSettings");
                var secretKey = jwtSettings.GetValue<string>("SecretKey");
                if (string.IsNullOrEmpty(secretKey))
                {
                    throw new InvalidOperationException("SecretKey is not configured properly in appsettings.json");
                }

                var key = Encoding.UTF8.GetBytes(secretKey);

                return new JwtBearerOptions
                {
                    TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                        ValidAudience = jwtSettings.GetValue<string>("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                    }
                };
            }).As<JwtBearerOptions>().SingleInstance();
            #endregion

            #region HttpContextAccessor Registration
            // Register HttpContextAccessor
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            #endregion

            #region Controller Registration
            // Register controllers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
                .AsSelf()
                .InstancePerLifetimeScope();
            #endregion

            #region ViewModel Validators Registration
            // Register validators for ViewModels
            builder.RegisterType<ConfirmEmailRequestViewModelValidator>()
                .As<IValidator<ConfirmEmailRequestViewModel>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RegisterUserRequestViewModelValidator>()
                .As<IValidator<RegisterUserRequestViewModel>>()
                .InstancePerLifetimeScope();
            #endregion

            #region Other Registrations
            builder.RegisterType<LogInInfoDTOValidator>()
                .As<IValidator<LogInInfoDTO>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserInfoFilter>().As<IActionFilter>().InstancePerLifetimeScope();
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope(); 
            #endregion
            builder.RegisterType<BaseRequestHandlerParameters>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<GetAllUsersQueryHandler>().As<IRequestHandler<GetAllUsersQuery, RequestResult<PaginatedResult<UserDTO>>>>().InstancePerLifetimeScope();
            // builder.RegisterType<TimeOutMiddleware>().InstancePerDependency();
            builder.RegisterType<GlobalErrorHandlerMiddleware>().InstancePerDependency();
            builder.RegisterType<CancellationTokenProvider>().AsSelf().SingleInstance();
            builder.RegisterType<CancelCommandInterceptor>().AsSelf().SingleInstance();
            
        }
    }
}
