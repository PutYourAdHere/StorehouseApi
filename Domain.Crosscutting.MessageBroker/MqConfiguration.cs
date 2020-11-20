namespace Domain.Crosscutting.MessageBroker
{
    public class MqConfiguration : IMqConfiguration
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
