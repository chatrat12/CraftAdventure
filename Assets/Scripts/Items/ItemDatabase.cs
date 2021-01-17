using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    private static Dictionary<int, Item> _items = new Dictionary<int, Item>();
    public static IEnumerable<Item> Items => _items.Values;

    static ItemDatabase()
    {
        Refresh();
    }

    public static Item GetItemFromID(int id)
    {
        if (_items.ContainsKey(id))
            return _items[id];
        return null;
    }

    public static T GetItemFromID<T>(int id) where T : Item
    {
        return GetItemFromID(id) as T;
    }


#if UNITY_EDITOR
    // Only used for tests in the editor.
    public static Item GetItemFromName(string itemName)
    {
        foreach (var pair in _items)
        {
            if (pair.Value.Name == itemName)
                return pair.Value;
        }
        return null;
    }
#endif

    public static void Refresh()
    {
        _items.Clear();
        var items = Resources.LoadAll<Item>("Items");
        foreach (var item in items)
            _items.Add(item.ID, item);
    }
}
