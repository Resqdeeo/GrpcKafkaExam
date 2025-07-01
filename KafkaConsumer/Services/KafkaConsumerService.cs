using System.Text.Json;
using Confluent.Kafka;
using KafkaConsumer.Models;

namespace KafkaConsumer.Services;

public class KafkaConsumerService
{
    private readonly ItemStore _store;

    public KafkaConsumerService(ItemStore store)
    {
        _store = store;
    }

    public void Start()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9093",
            GroupId = "consumer-group-1",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("item-topic");

        Console.WriteLine("Kafka Consumer started...");

        while (true)
        {
            var cr = consumer.Consume();
            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(cr.Message.Value);

            var action = json["Action"];
            var id = json["Id"];

            Task.Delay(2000).Wait(); // Симуляция делея в 2 секунды

            switch (action)
            {
                case "Add":
                case "Update":
                    var item = new Item
                    {
                        Id = id,
                        Name = json["Name"],
                        Description = json["Description"]
                    };
                    _store.AddOrUpdate(item);
                    break;
                case "Delete":
                    _store.Delete(id);
                    break;
            }

            ConsoleRenderer.RenderItems(_store.GetAll());
        }
    }
}