using UnityEngine;

[CreateAssetMenu(menuName = "CA/Items/Tool", fileName = "NewItemTool", order = 105)]
public class ItemTool : Item
{
    public ToolType Type => _type;

    [SerializeField] private ToolType _type;

    public enum ToolType
    {
        Axe,
        Pickaxe
    }
}
