using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIItemDisplayBase : UIView
{
    protected abstract ItemStack _displayItemStack { get; }

    //public ItemStack ItemStack
    //{
    //    get => _itemStack;
    //    set
    //    {
    //        _itemStack = value;
    //        Invalidate();
    //    }
    //}


    //protected ItemStack _itemStack = null;


    [SerializeField]
    protected Image _itemImage;
    [SerializeField]
    protected TextMeshProUGUI _itemName;
    [SerializeField]
    protected TextMeshProUGUI _itemCount;

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

    private void UpdateImage()
    {
        if (_itemImage == null) return;
        _itemImage.sprite = (_displayItemStack != null && _displayItemStack.Item != null) ? _displayItemStack.Item.Icon : null;
        _itemImage.enabled = _itemImage.sprite != null;
    }

    private void UpdateName()
    {
        if (_itemName == null) return;
        _itemName.text = (_displayItemStack != null && _displayItemStack.Item != null) ? _displayItemStack.Item.Name : string.Empty;
    }

    private void UpdateCount()
    {
        if (_itemCount == null) return;
        _itemCount.text = (_displayItemStack != null && _displayItemStack.Item != null && _displayItemStack.Count > 1) ? _displayItemStack.Count.ToString() : string.Empty;
    }
}