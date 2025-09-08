using MimeKit;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using MailKit.Net.Smtp;
namespace Project_Management_System.Features.Common;

public class EmailService
{
    public static async Task<RequestResult<bool>> SendEmail(string email, string name, string confirmationLink)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("UpSkilling Project", "upskillingfinalproject@gmail.com"));
        message.To.Add(new MailboxAddress(name, email));
        message.Subject = "Confirm Your Registration";

        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"Please confirm your registration using this token: [{confirmationLink}]",
            HtmlBody = $"<p>Please confirm your registration using this token: <a href='{confirmationLink}'>{confirmationLink}</a></p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using (var client = new SmtpClient())
            {
                client.Timeout = 10000;  
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("upskillingfinalproject@gmail.com", "vxfdhstkqegcfnei");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            return RequestResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError, ex.Message);
        }
    }
}