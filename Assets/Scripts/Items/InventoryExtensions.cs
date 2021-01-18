using System.Linq;

public partial class Inventory
{
    public bool HasTool(ItemTool.ToolType type)
        => _slots.Any(s => s.Item is ItemTool tool && tool.Type == type);
}
