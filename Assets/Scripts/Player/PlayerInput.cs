using UnityEngine;

public class PlayerInput
{
    public bool Disabled => _disabled;

    private bool _disabled = false;

    public Vector2 MovementVector { get; private set; }
    public bool Sprint { get; private set; }
    private Player _player;

    public PlayerInput(Player player)
        => _player = player;

    public void Update()
    {
        if (!_disabled)
        {
            if (_player.InteractDetection.AvailableInteraction != null && Input.GetKeyDown(KeyCode.F))
                _player.InteractDetection.AvailableInteraction.Interact(_player);
            if (Input.GetKeyDown(KeyCode.Space))
                _player.Animation.PickupAnimation();
        }
        MovementVector = _disabled ? Vector2.zero : GetInputMovementVector();

        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.Menu.Toggle();
            if (_player.Menu.Visible)
                Disable();
            else
                Enable();
        }
        Sprint = Input.GetKey(KeyCode.LeftShift);
    }

    private Vector2 GetInputMovementVector()
    {
        return new Vector2
        (
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;
    }

    public void Enable() => _disabled = false;
    public void Disable() => _disabled = true;
}
