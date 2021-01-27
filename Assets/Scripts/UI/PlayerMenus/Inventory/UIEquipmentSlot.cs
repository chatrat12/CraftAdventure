
public class UIEquipmentSlot : UIItemDisplayBase
{
    public EquipmentSlot Slot
    {
        get => _slot;
        set
        {
            _slot = value;
            _slot.OnEquipmentChanged.AddListener((type, newItem, oldItem) => UpdateImage());
        }
    }

    protected override Item _item => _slot.Item;

    private EquipmentSlot _slot;

    protected override void OnLeftClick()
    {
        base.OnLeftClick();

        void EquipItem(ItemEquipment equipment)
        {
            var oldItem = _slot.Equip(equipment);
            GameCursor.ItemStack.Item = oldItem;
            GameCursor.ItemStack.Count = 1;
            Invalidate();
        }

        if (GameCursor.ItemStack.Empty)
            EquipItem(null);
        else if (GameCursor.ItemStack.Item is ItemEquipment equipment)
            EquipItem(equipment);
    }
}