using UnityEngine;

public class UIPlayerOverlay : UIView
{
    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            _interactText.Player = _player;
        }
    }

    [SerializeField] UIInteractText _interactText; 
    private Player _player;
}
