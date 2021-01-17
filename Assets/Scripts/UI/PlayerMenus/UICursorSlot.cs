using UnityEngine;

public class UICursorSlot : UIView
{
    private UIItemDisplay _itemDisplay;
    private RectTransform _rect;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _itemDisplay = GetComponentInChildren<UIItemDisplay>();
        _itemDisplay.ItemStack = GameCursor.ItemStack;
    }

    private void LateUpdate()
    {
        _rect.anchoredPosition = Input.mousePosition;
    }
}
