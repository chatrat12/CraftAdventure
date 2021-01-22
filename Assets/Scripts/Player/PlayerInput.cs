using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    public bool Disabled => _disabled;

    private bool _disabled = false;

    public Vector3 MovementVector { get; private set; }
    public bool Sprint { get; private set; }
    private Player _player;

    public PlayerInput(Player player)
    {
        _player = player;
        Enable();
    }

    public void Update()
    {
        if (!_disabled)
        {
            if (_player.InteractDetection.AvailableInteraction != null && Keyboard.current.fKey.wasPressedThisFrame)
                _player.InteractDetection.AvailableInteraction.Interact(_player);
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                _player.Animation.ChopWood();
            HandleCamera();
        }
        MovementVector = _disabled ? Vector3.zero : GetMoveDirectionVector();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _player.Menu.Toggle();
            if (_player.Menu.Visible)
                Disable();
            else
                Enable();
        }
        Sprint = Keyboard.current.leftShiftKey.isPressed;
    }

    private void HandleCamera()
    {
        _player.CameraRig.Rotate(Mouse.current.delta.ReadValue() * 0.01f);
    }

    private Vector2 GetInputMovementVector()
    {
        float x = Keyboard.current.aKey.isPressed ? -1 : 0;
        x += Keyboard.current.dKey.isPressed ? 1 : 0;
        float y = Keyboard.current.wKey.isPressed ? 1 : 0;
        y += Keyboard.current.sKey.isPressed ? -1 : 0;
        return new Vector2(x, y).normalized;
    }

    private Vector3 GetMoveDirectionVector()
    {
        var input = GetInputMovementVector();
        var result = new Vector3(input.x, 0, input.y);
        result = Camera.main.transform.localToWorldMatrix.MultiplyVector(result);
        //Debug.Log($"{GetInputMovementVector()} {Vector3.ProjectOnPlane(result, Vector3.up).normalized}");
        result = Vector3.ProjectOnPlane(result, Vector3.up).normalized;
        Debug.DrawLine(_player.Avatar.transform.position, _player.Avatar.transform.position + result, Color.cyan);
        return result;
    }

    public void Enable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _disabled = false;
    }
    public void Disable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _disabled = true;
    }
}
