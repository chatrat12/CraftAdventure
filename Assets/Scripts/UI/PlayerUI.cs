using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            _menus.Player = _player;
            _overlays.Player = _player;
        }
    }

    public UIPlayerMenus Menus => _menus;
    public UIPlayerOverlay Overlays => _overlays;

    private Player _player;

    [SerializeField] UIPlayerMenus _menus;
    [SerializeField] UIPlayerOverlay _overlays;
}
