using Grpc.Net.Client;

namespace GrpcClient.Services;

public class ItemClientService
{
    private readonly ItemService.ItemServiceClient _client;
    
    public ItemClientService()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5280");
        _client = new ItemService.ItemServiceClient(channel);
    }

    public async Task AddItemAsync()
    {
        var id = Prompt("Enter ID:");
        var name = Prompt("Enter name:");
        var desc = Prompt("Enter description:");
        var res = await _client.AddItemAsync(new ItemRequest { Id = id, Name = name, Description = desc });
        Console.WriteLine(res.Status);
    }

    public async Task UpdateItemAsync()
    {
        var id = Prompt("Enter ID:");
        var name = Prompt("Enter new name:");
        var desc = Prompt("Enter new description:");
        var res = await _client.UpdateItemAsync(new ItemRequest { Id = id, Name = name, Description = desc });
        Console.WriteLine(res.Status);
    }

    public async Task DeleteItemAsync()
    {
        var id = Prompt("Enter ID to delete:");
        var res = await _client.DeleteItemAsync(new DeleteRequest { Id = id });
        Console.WriteLine(res.Status);
    }
    
    private string Prompt(string msg)
    {
        Console.Write(msg + " ");
        return Console.ReadLine();
    }
}