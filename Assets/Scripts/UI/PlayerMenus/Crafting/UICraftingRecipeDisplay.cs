using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UICraftingRecipeDisplay : UIItemDisplayBase, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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

    public UnityEvent OnClick { get; } = new UnityEvent();

    protected CraftingRecipe _recipe;
    protected override ItemStack _displayItemStack => _recipe.Result;

    public void OnPointerClick(PointerEventData eventData)
        => OnClick.Invoke();

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameCursor.Tooltip.Show();
        Recipe.BuildTooltip(GameCursor.Tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameCursor.Tooltip.Clear();
        GameCursor.Tooltip.Hide();
    }
}
