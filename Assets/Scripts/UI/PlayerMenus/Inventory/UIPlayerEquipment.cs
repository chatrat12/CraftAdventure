
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEquipment : UIView
{
    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            _slotEntries.Clear();
            DestroyChildren();
            foreach(var slot in _player.Equipment.Items)
            {
                var slotUI = Instantiate(_slotPrefab, transform);
                slotUI.Slot = slot;
                slotUI.ClickedRight.AddListener(() =>
                {
                    if (slot.Item != null && _player.Inventory.CanAddItem(slot.Item))
                        _player.Inventory.AddItem(slot.Equip(null));
                });
                _slotEntries.Add(slotUI);
            }
        }
    }

    [SerializeField] private UIEquipmentSlot _slotPrefab;

    private Player _player;
    private List<UIEquipmentSlot> _slotEntries = new List<UIEquipmentSlot>();
}
