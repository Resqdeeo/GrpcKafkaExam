using KafkaConsumer.Models;

namespace KafkaConsumer.Services;

public class ConsoleRenderer
{
    public static void RenderItems(IEnumerable<Item> items)
    {
        Console.Clear();
        Console.WriteLine("Current Items:");
        Console.WriteLine(new string('-', 40));
        foreach (var item in items)
        {
            Console.WriteLine($"ID: {item.Id}");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Description: {item.Description}");
            Console.WriteLine(new string('-', 40));
        }

        if (!items.Any())
        {
            Console.WriteLine("No items available.");
        }
    }
}