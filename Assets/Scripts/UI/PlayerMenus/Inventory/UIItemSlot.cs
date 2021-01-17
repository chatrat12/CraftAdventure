using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : Button
{
    public ItemStack ItemStack
    {
        get => _display.ItemStack;
        set => _display.ItemStack = value;
    }

    [SerializeField] UIItemDisplay _display;

    protected override void Awake()
    {
        onClick.AddListener(() =>
        {
            GameCursor.ItemStack.Swap(ItemStack);
        });
    }
    public void Invalidate(bool immediate = false)
        => _display.Invalidate(immediate);
}
