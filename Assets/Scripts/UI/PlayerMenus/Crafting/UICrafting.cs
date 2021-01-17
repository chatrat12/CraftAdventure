using UnityEngine;

public class UICrafting : UIView
{
    public Inventory Inventory { get; set; } = null;

    [SerializeField] private UICraftingRecipeDisplay _recipeButtonPrefab;
    [SerializeField] private Transform _buttonParent;

    private void Awake()
    {
        foreach(var recipe in CraftingRecipeDatabase.Recipes)
        {
            var button = Instantiate(_recipeButtonPrefab, _buttonParent);
            button.Recipe = recipe;
            button.OnClick.AddListener(() =>
            {
                if(Inventory == null)
                {
                    Debug.Log("Inventory is null");
                    return;
                }
                var result = button.Recipe.Craft(Inventory);
                Debug.Log(result ? $"Crafted {button.Recipe.Result}" : "Missing Ingrediants");
            });
        }
    }

}
