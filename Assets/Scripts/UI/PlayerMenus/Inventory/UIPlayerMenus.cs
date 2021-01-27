using UnityEngine;

public class UIPlayerMenus : UIView
{
    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            _inventoryUI.Inventory = _player.Inventory;
            _crafting.Inventory = _player.Inventory;
            _equipment.Player = _player;

            _inventoryUI.OnItemEquip.AddListener((slot) =>
            {
                Debug.Log("Do it");
                var oldItem = value.Equipment.Items.Equip(slot.ItemStack.Item as ItemEquipment);
                slot.ItemStack.Item = oldItem;
            });
        }
    }

    [SerializeField] private UIInventory _inventoryUI;
    [SerializeField] private ItemStack[] _startingItems;
    [SerializeField] private UICrafting _crafting;
    [SerializeField] private UIPlayerEquipment _equipment;
    private Player _player;
}