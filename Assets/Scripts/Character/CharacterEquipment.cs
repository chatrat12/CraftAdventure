using System.Collections;
using System.Collections.Generic;
public class CharacterEquipment : IEnumerable<EquipmentSlot>
{
    public IEnumerable<KeyValuePair<EquipmentSlotType, EquipmentSlot>> Slots => _slots;

    private Dictionary<EquipmentSlotType, EquipmentSlot> _slots = new Dictionary<EquipmentSlotType, EquipmentSlot>();

    public CombatStats StatBoosts
    {
        get
        {
            var result = new CombatStats();
            foreach (var slot in _slots.Values)
                result += slot.StatModifier;
            return result;
        }
    }

    public EquipmentSlot GetSlot(EquipmentSlotType slotType)
    {
        if (_slots.ContainsKey(slotType))
            return _slots[slotType];
        return null;
    }
    public ItemEquipment Equip(ItemEquipment equipment)
    {
        if (_slots.ContainsKey(equipment.SlotType))
            return _slots[equipment.SlotType].Equip(equipment);
        return null;
    }

    public EquipmentSlot AddEquipmentSlot(EquipmentSlotType slotType)
    {
        var slot = new EquipmentSlot(slotType);
        _slots.Add(slotType, slot);
        return slot;
    }

    public IEnumerator<EquipmentSlot> GetEnumerator() => _slots.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}