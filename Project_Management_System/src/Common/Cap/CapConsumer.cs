using DotNetCore.CAP;

namespace Project_Management_System.src.Common.Cap
{
    public class CapConsumer : ICapSubscribe
    {
        [CapSubscribe("Adel")]
        public Task HandleProjectAdded(BasicMessage<string> message)
        {
            Console.WriteLine($"Received message from {message.Sender} at {message.Date}: {message.Data}");
            return Task.CompletedTask;
        }
    }
}
