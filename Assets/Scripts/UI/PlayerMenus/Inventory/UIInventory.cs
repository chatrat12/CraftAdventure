
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIView
{
    public Inventory Inventory
    {
        get => _inventory;
        set
        {
            _inventory = value;
            _slotEntries.Clear();
            foreach (Transform child in _slotParent)
                Destroy(child.gameObject);
            foreach(var slot in _inventory.Slots)
            {
                var slotUI = Instantiate(_slotPrefab, _slotParent);
                slotUI.ItemStack = slot;
                _slotEntries.Add(slotUI);
            }
        }
    }

    [SerializeField] private UIItemSlot _slotPrefab;
    [SerializeField] private Transform _slotParent;
    private Inventory _inventory;
    private List<UIItemSlot> _slotEntries = new List<UIItemSlot>();

    protected override void OnInvalidated()
    {
        foreach (var slot in _slotEntries)
            slot.Invalidate(true);
    }

    private void OnEnable() => Invalidate(true);

}
