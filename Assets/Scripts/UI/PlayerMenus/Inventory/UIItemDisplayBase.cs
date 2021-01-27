using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIItemDisplayBase : UIView, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent ClickedLeft { get; } = new UnityEvent();
    public UnityEvent ClickedRight { get; } = new UnityEvent();

    protected abstract Item _item { get; }
    protected virtual int _count => 1;

    [SerializeField] protected Image _itemImage;
    [SerializeField] protected TextMeshProUGUI _itemName;
    [SerializeField] protected TextMeshProUGUI _itemCount;

    private void Awake()
    {
        Invalidate();
    }

    protected override void OnInvalidated()
    {
        UpdateImage();
        UpdateName();
        UpdateCount();
    }

    protected void UpdateImage()
    {
        if (_itemImage == null) return;
        _itemImage.overrideSprite = (_item != null) ? _item.Icon : null;
        _itemImage.enabled = _itemImage.sprite != null || _itemImage.overrideSprite != null;
    }

    protected void UpdateName()
    {
        if (_itemName == null) return;
        _itemName.text = (_item != null) ? _item.Name : string.Empty;
    }

    protected void UpdateCount()
    {
        if (_itemCount == null) return;
        _itemCount.text = (_count > 1) ? _count.ToString() : string.Empty;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (_item != null)
        {
            GameCursor.Tooltip.Show();
            _item.BuildTooltip(GameCursor.Tooltip);
        }
        else
            GameCursor.Tooltip.Hide();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        GameCursor.Tooltip.Clear();
        GameCursor.Tooltip.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClick();
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClick();
    }

    protected virtual void OnLeftClick() => ClickedLeft.Invoke();
    protected virtual void OnRightClick() => ClickedRight.Invoke();
}