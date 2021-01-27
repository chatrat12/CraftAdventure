public class UIItemDisplay : UIItemDisplayBase
{
    public virtual ItemStack ItemStack
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
    protected override Item _item => _itemStack?.Item;
    protected override int _count => (_itemStack != null && _itemStack.Item != null) ? _itemStack.Count : 0;

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
