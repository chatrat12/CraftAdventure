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
        }
    }

    [SerializeField] private UIInventory _inventoryUI;
    [SerializeField] private ItemStack[] _startingItems;
    [SerializeField] private UICrafting _crafting;
    private Player _player;
}