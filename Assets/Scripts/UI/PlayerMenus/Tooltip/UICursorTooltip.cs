﻿using UnityEngine;

public class UICursorTooltip : UITooltip
{
    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        GameCursor.RegisterTooltip(this);
    }

    private void LateUpdate()
    {
        _rect.anchoredPosition = Input.mousePosition;
    }

}
