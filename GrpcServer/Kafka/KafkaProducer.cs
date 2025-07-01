using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace GrpcServer.Kafka;

public class KafkaProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _bootstrapServers = "localhost:9093";

    public KafkaProducer(IConfiguration config)
    {
        var conf = new ProducerConfig { BootstrapServers = _bootstrapServers };
        _producer = new ProducerBuilder<Null, string>(conf).Build();
    }

    public async Task SendAsync(string topic, string message)
    {
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
    }
    
    public async Task CreateTopicIfNotExistsAsync(string topicName)
    {
        var config = new AdminClientConfig { BootstrapServers = _bootstrapServers };
        using var adminClient = new AdminClientBuilder(config).Build();

        try
        {
            await adminClient.CreateTopicsAsync(new[] {
                new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = 1,
                    ReplicationFactor = 1
                }
            });
        }
        catch (CreateTopicsException e)
        {
            if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                throw;
        }
    }
}