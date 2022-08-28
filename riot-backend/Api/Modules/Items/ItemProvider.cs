using System.Text;
using Newtonsoft.Json;
using riot_backend.Api.Modules.Items.Types;

namespace riot_backend.Api.Modules.Items;

public class ItemProvider
{
    private List<Item> items { get; }

    public ItemProvider()
    {
        var path = AppContext.BaseDirectory + "/wwwroot/items.json";
        using var streamReader = new StreamReader(path, Encoding.UTF8);
        var contents = streamReader.ReadToEnd();
        items = JsonConvert.DeserializeObject<List<Item>>(contents) ??
                throw new InvalidOperationException("Could not decode file");
    }


    public List<Item> GetAll()
    {
        return items;
    }

    public Item Get(string key)
    {
        var itemEnumerable = items.Where(item => item.id == key);
        if (itemEnumerable.Count() == 1)
        {
            return itemEnumerable.GetEnumerator().Current;
        }

        throw new KeyNotFoundException();
    }
}