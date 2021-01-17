
using System;

public class UIItemDisplay : UIItemDisplayBase
{
    public ItemStack ItemStack
    {
        get => _itemStack;
        set
        {
            if (_itemStack != null)
            {
                _itemStack.ItemChanged -= ItemUpdated;
                _itemStack.CountChanged -= ItemUpdated;
            }
            if (value != null)
            {
                value.ItemChanged += ItemUpdated;
                value.CountChanged += ItemUpdated;
            }
            _itemStack = value;
            Invalidate(true);
        }
    }

    private void ItemUpdated(ItemStack sender)
    {
        Invalidate(true);
    }

    protected ItemStack _itemStack = null;
    protected override ItemStack _displayItemStack => _itemStack;

    protected void OnDestroy()
    {
#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlaying)
            return;
#endif
        if (ItemStack != null)
        {
            ItemStack.ItemChanged -= ItemUpdated;
            ItemStack.CountChanged -= ItemUpdated;
        }
    }
}
