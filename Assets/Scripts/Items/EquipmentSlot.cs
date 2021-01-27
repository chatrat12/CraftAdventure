using UnityEngine.Events;

public class EquipmentSlot
{
    public class ChangeEvent : UnityEvent<EquipmentSlotType, ItemEquipment, ItemEquipment> { }
    public ChangeEvent OnEquipmentChanged { get; } = new ChangeEvent();

    public ItemEquipment Item { get; private set; }
    public CombatStats StatModifier
    {
        get
        {
            if (Item != null)
                return Item.StatModifier;
            return CombatStats.Empty;
        }
    }

    public EquipmentSlotType Type { get; private set; }

    public EquipmentSlot(EquipmentSlotType type)
    {
        Type = type;
    }

    public ItemEquipment Equip(ItemEquipment newItem)
    {
        var oldItem = Item;
        Item = newItem;
        OnEquipmentChanged.Invoke(Type, newItem, oldItem);
        return oldItem;
    }
}
