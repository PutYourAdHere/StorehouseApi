
namespace Domain.Crosscutting.MessageBroker
{
    public interface IMqConfiguration
    {
        string HostName { get; set; }
        
        string QueueName { get; set; }

        string User { get; set; }

        string Password { get; set; }
    }
}
