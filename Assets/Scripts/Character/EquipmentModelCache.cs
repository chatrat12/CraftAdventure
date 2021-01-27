using System.Collections.Generic;
using UnityEngine;

public class EquipmentModelCache
{
    private Dictionary<ItemEquipment, EquipmentComponent> _modelCache = new Dictionary<ItemEquipment, EquipmentComponent>();

    public EquipmentComponent GetModel(ItemEquipment item)
    {
        if (_modelCache.ContainsKey(item))
            return _modelCache[item];
        var result = GameObject.Instantiate(item.Model);
        result.Item = item;
        _modelCache.Add(item, result);
        return result;
    }
}
