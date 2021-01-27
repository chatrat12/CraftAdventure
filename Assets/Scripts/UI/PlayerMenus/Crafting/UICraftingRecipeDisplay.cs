using UnityEngine.EventSystems;

public class UICraftingRecipeDisplay : UIItemDisplayBase
{
    public CraftingRecipe Recipe
    {
        get => _recipe;
        set
        {
            _recipe = value;
            Invalidate();
        }
    }

    protected override Item _item => _recipe.Result.Item;
    protected override int _count => _recipe.Result.Count;

    protected CraftingRecipe _recipe;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GameCursor.Tooltip.Show();
        Recipe.BuildTooltip(GameCursor.Tooltip);
    }
}
