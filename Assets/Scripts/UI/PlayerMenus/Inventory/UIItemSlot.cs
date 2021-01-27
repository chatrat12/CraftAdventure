public class UIItemSlot : UIItemDisplay
{
    protected override void OnLeftClick()
    {
        base.OnLeftClick();
        GameCursor.ItemStack.Swap(ItemStack);
    }
}