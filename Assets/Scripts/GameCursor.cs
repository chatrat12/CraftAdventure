public class GameCursor
{
    //public delegate void StackChangeEvent(ItemStack oldStack, ItemStack newStack);
    //public static event StackChangeEvent ItemStackChanged;

    public static ItemStack ItemStack { get; } = new ItemStack();

    public static UITooltip Tooltip { get; private set; }

    public static void RegisterTooltip(UITooltip tooltip)
    {
        Tooltip = tooltip;
    }
}
