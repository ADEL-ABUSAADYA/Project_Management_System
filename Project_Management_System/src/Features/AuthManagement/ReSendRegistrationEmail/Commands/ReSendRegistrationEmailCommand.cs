// using MediatR;
// using MailKit.Net.Smtp;
// using MimeKit;
// using Project_Management_System.Common.BaseHandlers;
// using Project_Management_System.Common.Data.Enums;
// using Project_Management_System.Common.Views;
// using Project_Management_System.Data.Repositories;
// using Project_Management_System.Features.AuthManagement.ReSendRegistrationEmail.Queries;
// using Project_Management_System.Models;
//
//
// namespace Project_Management_System.Features.AuthManagement.ReSendRegistrationEmail.Commands;
//
// public record ReSendRegistrationEmailCommand(string email) : IRequest<RequestResult<bool>>;
//
// public class ReSendRegistrationEmailCommandHandler : BaseRequestHandler<ReSendRegistrationEmailCommand, RequestResult<bool>>
// {
//     private readonly IRepository<User> _repository;
//     public ReSendRegistrationEmailCommandHandler(BaseRequestHandlerParameters parameters, IRepository<User> repository) : base(parameters)
//     {
//         _repository = repository;
//     }
//     
//     public override async Task<RequestResult<bool>> Handle(ReSendRegistrationEmailCommand request,
//         CancellationToken cancellationToken)
//     {
//         var RegisterationData = await _mediator.Send(new GetUserRegistrationInfoQuery(request.email));
//
//         if (!RegisterationData.isSuccess)
//             return RequestResult<bool>.Failure(RegisterationData.errorCode, RegisterationData.message);
//         
//         if (RegisterationData.data.IsEmailConfirmed)
//             return RequestResult<bool>.Failure(ErrorCode.UserAlreadyRegistered, "user already registered please login");
//   
//         var confirmationLink = $"{RegisterationData.data.ConfirmationToken}";
//         
//         var emailSent = await SendConfirmationEmail(RegisterationData.data.Email, RegisterationData.data.Name, confirmationLink);
//         if (!emailSent.isSuccess)
//             return RequestResult<bool>.Failure(ErrorCode.EmailNotSent);
//
//         return RequestResult<bool>.Success(true);
//     }
//     
//     private async Task<RequestResult<bool>> SendConfirmationEmail(string email, string name, string confirmationLink)
//     {
//         var message = new MimeMessage();
//         message.From.Add(new MailboxAddress("adel", "upskillingfinalproject@gmail.com"));
//         message.To.Add(new MailboxAddress(name, email));
//         message.Subject = "UpSkilling Final Project";
//
//         // Create multipart content for both plain text and HTML
//         var bodyBuilder = new BodyBuilder
//         {
//             TextBody = $"Please confirm your registration by token [{confirmationLink}]",
//             HtmlBody = $"Please confirm your registration by token [{confirmationLink}]"
//         };
//
//         message.Body = bodyBuilder.ToMessageBody();
//
//         try
//         {
//             using (var client = new SmtpClient())
//             {
//                 // Set the timeout for connection and authentication
//                 client.Timeout = 10000;  // Timeout after 10 seconds
//             
//                 // Connect using StartTLS for security
//                 await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
//
//                 // Authenticate with the provided credentials
//                 await client.AuthenticateAsync("upskillingfinalproject@gmail.com", "vxfdhstkqegcfnei");
//
//                 // Send the email
//                 await client.SendAsync(message);
//                 await client.DisconnectAsync(true);
//             }
//
//             return RequestResult<bool>.Success(true);
//         }
//         catch (Exception ex)
//         {
//             // Log the detailed exception message for debugging
//             Console.WriteLine($"Error sending email: {ex.Message}");
//             Console.WriteLine($"Stack Trace: {ex.StackTrace}");
//
//             // Return failure with error details
//             return RequestResult<bool>.Failure(ErrorCode.UnKnownError, ex.Message);
//         }
//     }
// }
//
