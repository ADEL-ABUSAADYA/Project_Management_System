using System.Text;
using System.Text.Json;
using DotNetCore.CAP;
using MailKit.Net.Smtp;
using MimeKit;
using Project_Management_System.Common.Data.Enums;
using Project_Management_System.Common.Views;
using Project_Management_System.Models;
using Project_Management_System.src.Helpers;
using RabbitMQ.Client.Events;

namespace Project_Management_System.Features.AuthManagement.RegisterUser.Consumers;

public class UserRegisteredEventConsumer : ICapSubscribe
{
    [CapSubscribe("user.registered")]
    public async Task HandleUserRegisteredAsync(string messageJson)
    {
        try
        {
            var userEvent = JsonSerializer.Deserialize<UserRegisteredEvent>(messageJson);
            if (userEvent == null)
            {
                Console.WriteLine("Failed to deserialize UserRegisteredEvent.");
                throw new Exception("UserRegisteredEvent deserialization failed.");
            }

            var confirmationLink = $"{userEvent.ActivationLink}";
            var result = await EmailHelper.SendEmail(userEvent.Email, userEvent.Name, confirmationLink);

            if (!result.isSuccess)
            {
                throw new Exception($"Failed to send confirmation email: {result.message}");
            }

            Console.WriteLine($"Confirmation email sent to {userEvent.Email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing user.registered event: {ex.Message}");
            throw; // Re-throw the exception so CAP retries the message
        }
    }
    
}
