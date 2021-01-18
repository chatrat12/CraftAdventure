using System.Collections.Generic;
using UnityEngine;

public partial class Inventory
{
    public IEnumerable<ItemStack> Slots => _slots;
    public int SlotCount { get { return _slots.Length; } }

    public int FreeSlots
    {
        get
        {
            int result = 0;
            foreach (var slot in _slots)
                if (!slot.Empty)
                    result++;
            return result;
        }
    }

    private ItemStack[] _slots;

    public Inventory(int slotCount)
    {
        Initialize(slotCount);
    }

    private void Initialize(int slotCount)
    {
        _slots = new ItemStack[slotCount];
        for (int i = 0; i < slotCount; i++)
            _slots[i] = new ItemStack();
    }

    public void AddItem(ItemStack stack)
    {
        AddItem(stack.Item, stack.Count);
    }
    public void AddItem(Item item, int count = 1)
    {
        if (item != null)
        {
            int transferAmount;

            // first check for slots that already contain the item
            foreach (var slot in Slots)
            {
                if (slot.Item == item)
                {
                    transferAmount = Mathf.Min(count, slot.FreeSpace);
                    count -= transferAmount;
                    slot.Count += transferAmount;
                    if (count == 0) return;
                }
            }
            // check for free slots to put item in
            foreach (var slot in Slots)
            {
                if (slot.Item == null)
                {
                    slot.Item = item;
                    transferAmount = Mathf.Min(count, slot.Item.MaxStack);
                    count -= transferAmount;
                    slot.Count = transferAmount;
                    if (count == 0) return;
                }
            }
            // TO DO: Figure out what to do with remaining. Return amount left?
        }
    }

    public bool CanAddItem(ItemStack itemStack)
    {
        if (itemStack == null || itemStack.Empty)
            return false;
        return CanAddItem(itemStack.Item, itemStack.Count);
    }

    public bool CanAddItem(Item item, int count = 1)
    {
        if (item == null || count < 1)
            return false;
        // first check for slots that already contain the item
        foreach (var slot in Slots)
        {
            if (slot.Item == item)
            {
                count -= slot.FreeSpace;
                if (count <= 0) return true;
            }
        }
        // check for free slots to put item in
        foreach (var slot in Slots)
        {
            if (slot.Item == null)
            {
                count -= item.MaxStack;
                if (count <= 0) return true;
            }
        }
        return false;
    }
    public void RemoveItem(Item item, int count = 1)
    {
        foreach (var slot in Slots)
        {
            if (slot.Item == item)
            {
                if (count >= slot.Count)
                {
                    count -= slot.Count;
                    slot.Item = null;
                    if (count == 0) return;
                }
                else
                {
                    slot.Count -= count;
                    return;
                }
            }
        }
    }
    public void RemoveItem(ItemStack stack)
    {
        RemoveItem(stack.Item, stack.Count);
    }

    public void RemoveItems(IEnumerable<ItemStack> stacks)
    {
        foreach (var stack in stacks)
            RemoveItem(stack);
    }

    public void Clear()
    {
        foreach (var slot in _slots)
        {
            slot.Item = null;
            slot.Count = 0;
        }
    }

    public bool HasItem(Item item)
    {
        return HasItem(item, 1);
    }
    public bool HasItem(Item item, int count)
    {
        int tally = 0;
        foreach (var slot in Slots)
        {
            if (slot.Item == item)
            {
                tally += slot.Count;
                if (tally >= count)
                    return true;
            }
        }
        return false;
    }
    public bool HasItem(ItemStack stack)
    {
        return HasItem(stack.Item, stack.Count);
    }
    public bool HasItems(IEnumerable<ItemStack> stacks)
    {
        foreach(var stack in stacks)
        {
            if (!HasItem(stack))
                return false;
        }
        return true;
    }

    public override string ToString()
    {
        return string.Format("Slots: {0}/{1}", SlotCount - FreeSlots, SlotCount);
    }
}