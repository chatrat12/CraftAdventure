using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public delegate void ItemStackEvent(ItemStack sender);
    public event ItemStackEvent ItemChanged;
    public event ItemStackEvent CountChanged;

    // Used for swapping Item stacks.
    private static ItemStack _tempItemStack = new ItemStack();

    public Item Item
    {
        get { return _item; }
        set
        {
            if (value != _item)
            {
                _item = value;
                if (ItemChanged != null)
                    ItemChanged(this);
            }
        }
    }
    public int Count
    {
        get { return _count; }
        set
        {
            if (value != _count)
            {
                _count = value;
                if (CountChanged != null)
                    CountChanged(this);
            }
        }
    }
    public bool Empty { get { return Item == null || Count <= 0; } }
    public bool Full { get { return Item != null && Count == Item.MaxStack; } }

    [SerializeField] private Item _item = null;
    [SerializeField] private int _count = 1;

    public int FreeSpace
    {
        get
        {
            if (Item != null)
            {
                return Item.MaxStack - Count;
            }
            // Item was null, return 0
            return 0;
        }
    }

    public ItemStack() { }
    public ItemStack(Item item, int count = 1) => Set(item, count);

    public void Set(Item newItem, int newCount)
    {
        Item = newItem;
        Count = newCount;
    }
    // Sets item to null if count hit zero
    public void Deplete(int count)
    {
        Count = Mathf.Max(Count - count, 0);
        if (Count == 0)
            Item = null;
    }
    public void CopyTo(ItemStack target)
    {
        target.Item = Item;
        target.Count = Count;
    }
    public void CopyFrom(ItemStack target) => target.CopyTo(this);
    public void Swap(ItemStack target) => Swap(this, target);
    public void Deposit(ItemStack depositTo, int count) => Deposit(this, depositTo, count);
    public void DepositAll(ItemStack depositTo) => DepositAll(this, depositTo);
    public void Clear() => Set(null, 0);
    public override bool Equals(object obj)
    {
        var other = obj as ItemStack;

        if (other == null)
            return false;

        return ReferenceEquals(_item, other._item) &&
               _count == other.Count;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + (Item != null ? Item.ID : -1);
            hash = hash * 23 + _count;
            return hash;
        }
    }
    public override string ToString()
    {
        if (_item == null)
            return "Null";
        var countString = _count > 1 ? string.Format(" x{0}", _count) : string.Empty;
        return string.Format("{0}{1}", _item.Name, countString);
    }

    public static void Swap(ItemStack stackA, ItemStack stackB)
    {
        stackA.CopyTo(_tempItemStack);
        stackB.CopyTo(stackA);
        _tempItemStack.CopyTo(stackB);
    }
    public static void Deposit(ItemStack from, ItemStack to, int count)
    {
        if (CamDeposit(from, to))
        {
            // If 'to stack' is empty, set its item type
            if (to.Empty)
                to.Item = from.Item;
            // Find maximum we can transfer
            var transferCount = Mathf.Min(from.Count, to.FreeSpace, count);
            // Add transfer count to 'to stack'
            to.Count += transferCount;
            // Remove transfer stack from the 'from stack'
            from.Count -= transferCount;
            // If none left in 'from stack', null 'from stack' item
            if (from.Count <= 0)
                from.Item = null;
        }
    }
    public static void DepositAll(ItemStack from, ItemStack to)
    {
        Deposit(from, to, from.Count);
    }
    public static bool CamDeposit(ItemStack from, ItemStack to)
    {
        // 'From stack' must not be empty
        // 'To stack' must be empty or it must have
        // the same item type as the 'from stack' and
        // have free space left.
        return !from.Empty && (to.Empty || (to.Item == from.Item) && to.FreeSpace > 0);
    }
}