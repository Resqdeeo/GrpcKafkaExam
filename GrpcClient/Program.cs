using GrpcClient.Services;

var service = new ItemClientService();

while (true)
{
    Console.WriteLine("\n1 - Add\n2 - Update\n3 - Delete\n0 - Exit");
    var input = Console.ReadLine();

    try
    {
        switch (input)
        {
            case "1":
                await service.AddItemAsync();
                break;
            case "2":
                await service.UpdateItemAsync();
                break;
            case "3":
                await service.DeleteItemAsync();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid input.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}