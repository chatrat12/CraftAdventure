
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITooltip : UIView
{
    [SerializeField] TMP_FontAsset _font;
    [SerializeField] UIItemDisplay _itemDisplayPrefab;

    private void Start()
    {
        Clear();
        Hide();
    }

    public void AddTitle(string text, Color? color = null)
        => CreateText(text, color, 36, true, transform);
    public void AddLine(string text, Color? color = null)
        => CreateNormalText(text, color);
    public void AddItem(ItemStack stack, Color? color = null)
    {
        var display = Instantiate(_itemDisplayPrefab);
        display.ItemStack = stack;
        CreatLineGroup(12, display.transform, CreateNormalText(stack.Item.Name, color).transform);
    }
    public void AddStat(string statName, string statValue, Color? color = null)
    {
        CreatLineGroup(12, CreateNormalText(statName, color).transform, 
                           CreateNormalText(statValue, color).transform);
    }

    public void Clear()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    protected TextMeshProUGUI CreateNormalText(string text, Color? color)
        => CreateText(text, color, 28, false, transform);

    protected TextMeshProUGUI CreateText(string text, Color? color, float fontSize, bool bold, Transform parent)
    {
        if (color == null)
            color = Color.white;

        var go = new GameObject("Text");
        go.transform.SetParent(parent);
        var tm = go.AddComponent<TextMeshProUGUI>();
        tm.font = _font;
        tm.text = text;
        tm.color = color.Value;
        tm.fontSize = fontSize;
        tm.fontStyle = bold ? FontStyles.Bold : FontStyles.Normal;
        tm.verticalAlignment = VerticalAlignmentOptions.Capline;

        return tm;
    }

    protected HorizontalLayoutGroup CreatLineGroup(float spacing, params Transform[] children)
    {
        var go = new GameObject("Line");
        go.transform.SetParent(transform);
        var group = go.AddComponent<HorizontalLayoutGroup>();
        group.childForceExpandWidth = false;
        group.childForceExpandHeight = true;
        group.childControlWidth = true;
        group.childControlHeight = true;
        group.spacing = spacing;
        foreach (var child in children)
            child.SetParent(group.transform);
        return group;
    }
}
