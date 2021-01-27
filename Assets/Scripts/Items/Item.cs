using System;
using UnityEngine;

[CreateAssetMenu(menuName = "CA/Items/Item", fileName = "NewItem", order = 105)]
public class Item : IndentifiableScriptableObject
{
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public int SellValue => Mathf.FloorToInt(Cost * 0.75f);
    public int MaxStack => _maxStack;
    public Sprite Icon => _icon;

    [SerializeField] private string _name = "Untitled Item";
    [SerializeField] private Sprite _icon = null;
    [TextArea]
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private int _maxStack = 1;

    public virtual void BuildTooltip(UITooltip tooltip)
    {
        tooltip.Clear();
        tooltip.AddLine(Name);
        BuildTooltipStats(tooltip);
        if(!string.IsNullOrEmpty(Description))
            tooltip.AddLine(Description);
    }

    protected virtual void BuildTooltipStats(UITooltip tooltip) { }

    public override bool Equals(object obj)
    {
        var other = obj as Item;
        if (other == null)
            return false;
        return ID == other.ID;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ID;
        }
    }
}

