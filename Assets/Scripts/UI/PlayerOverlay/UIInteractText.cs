using System;
using TMPro;

public class UIInteractText : UIView
{
    private TextMeshProUGUI _text;

    public Player Player { get; set; }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Player != null)
        {
            _text.enabled = !Player.Menu.Visible;
            _text.text = Player.InteractDetection.AvailableInteraction != null ?
                         Player.InteractDetection.AvailableInteraction.InteractMessage : string.Empty;
        }
        else
            _text.text = string.Empty;
    }
}
