using System.Collections.Generic;
using UnityEngine;

public static class CraftingRecipeDatabase
{
    public static IEnumerable<CraftingRecipe> Recipes => _recipes.Values;
    private static Dictionary<int, CraftingRecipe> _recipes = new Dictionary<int, CraftingRecipe>();

    static CraftingRecipeDatabase()
    {
        Refresh();
    }

    public static CraftingRecipe GetRecipeFromID(int id)
    {
        if (_recipes.ContainsKey(id))
            return _recipes[id];
        return null;
    }

    private static void Refresh()
    {
        _recipes.Clear();
        var recipes = Resources.LoadAll<CraftingRecipe>("CraftingRecipes");
        foreach (var recipe in recipes)
            _recipes.Add(recipe.ID, recipe);
    }
}