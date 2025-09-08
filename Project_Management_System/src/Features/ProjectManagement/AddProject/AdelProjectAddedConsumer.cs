using DotNetCore.CAP;

namespace Project_Management_System.src.Features.ProjectManagement.AddProject
{
    public class AdelProjectAddedConsumer : ICapSubscribe
    {
        [CapSubscribe("adel")]
        public Task HandleProjectAdded(string message)
        {
           
               
        }
    }
}
