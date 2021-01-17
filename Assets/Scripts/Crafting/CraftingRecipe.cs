using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CA/Crafting Recipe")]
public class CraftingRecipe : IndentifiableScriptableObject
{
    public IEnumerable<ItemStack> Requirements => _requirements;
    public ItemStack Result => _result;

    [SerializeField] ItemStack[] _requirements;
    [SerializeField] ItemStack _result;

    public bool CanCraft(Inventory inventory)
        => inventory.HasItems(_requirements);

    public bool Craft(Inventory inventory)
    {
        if(CanCraft(inventory))
        {
            inventory.RemoveItems(_requirements);
            inventory.AddItem(_result);
            return true;
        }
        return false;
    }
    
    public void BuildTooltip(UITooltip tooltip)
    {
        tooltip.AddTitle(_result.ToString());
        foreach (var req in _requirements)
            tooltip.AddItem(req);
    }
}
