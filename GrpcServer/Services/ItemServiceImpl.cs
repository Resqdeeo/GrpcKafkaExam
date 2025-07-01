using System.Text.Json;
using Grpc.Core;
using GrpcServer.Kafka;

namespace GrpcServer.Services;

public class ItemServiceImpl : ItemService.ItemServiceBase
{
    private readonly KafkaProducer _producer;
    private const string Topic = "item-topic";

    public ItemServiceImpl(KafkaProducer producer) => _producer = producer;

    public override async Task<ItemResponse> AddItem(ItemRequest request, ServerCallContext context)
    {
        var json = JsonSerializer.Serialize(new { Action = "Add", request.Id, request.Name, request.Description });
        await _producer.SendAsync(Topic, json);
        return new ItemResponse { Status = "Add Queued" };
    }

    public override async Task<ItemResponse> UpdateItem(ItemRequest request, ServerCallContext context)
    {
        var json = JsonSerializer.Serialize(new { Action = "Update", request.Id, request.Name, request.Description });
        await _producer.SendAsync(Topic, json);
        return new ItemResponse { Status = "Update Queued" };
    }

    public override async Task<ItemResponse> DeleteItem(DeleteRequest request, ServerCallContext context)
    {
        var json = JsonSerializer.Serialize(new { Action = "Delete", request.Id });
        await _producer.SendAsync(Topic, json);
        return new ItemResponse { Status = "Delete Queued" };
    }
}