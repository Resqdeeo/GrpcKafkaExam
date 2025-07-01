using System.Collections.Concurrent;
using KafkaConsumer.Models;

namespace KafkaConsumer.Services;

public class ItemStore
{
    private readonly ConcurrentDictionary<string, Item> _items = new();

    public void AddOrUpdate(Item item) => _items[item.Id] = item;
    public void Delete(string id) => _items.TryRemove(id, out _);
    public IReadOnlyCollection<Item> GetAll() => _items.Values.ToList();
}