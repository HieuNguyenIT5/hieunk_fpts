namespace Account.Domain.AggregateModels;
public class KafkaSettings
{
    public string HostPort { get; set; }
    public string Group    { get; set; }
    public string Topic    { get; set; }
}
