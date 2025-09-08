using DotNetCore.CAP;

namespace Project_Management_System.src.Features.ProjectManagement.AddProject
{
    public class ProjectAddedConsumer : ICapSubscribe
    {
        [CapSubscribe("adel")]
        public Task HandleProjectAdded(string message)
        {
            Console.WriteLine($"Received message: {message}");
            // Add your logic to handle the event here
            return Task.CompletedTask;
        }
    }
}
