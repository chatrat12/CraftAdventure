using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform Target { get { return _target; } set { _target = value; } }

    [Header("Turn")]
    [Tooltip("Degrees per second. Speed at which the camera moves around the target.")]
    [SerializeField]
    private float _turnSpeed = 180f;
    [Tooltip("Degrees per second. Speed at which the camera auto turns.")]
    [SerializeField]
    private float _autoTurnSpeed = 45f;

    [Header("Tilt")]
    [Tooltip("Degrees. The maximum value of the x axis rotation of the pivot.")]
    [SerializeField]
    private float _tiltMin = 45f;
    [Tooltip("Degrees. The minimum value of the x axis rotation of the pivot.")]
    [SerializeField]
    private float _tileMax = 75f;
    [Tooltip("Degrees. Target tile angle.")]
    [SerializeField]
    private float _targetTilt = 15f;
    [Tooltip("Seconds. Time it takes for tilt to reset to tilt target.")]
    [SerializeField]
    private float _autoTiltTime = 1f;

    [Header("Input")]
    [Tooltip("Invert Y Axis.")]
    [SerializeField]
    private bool _invertYAxis = false;

    [Header("Misc")]
    [Tooltip("Input idle time required to enable AutoOrientation")]
    [SerializeField]
    private float _idleTimeBeforeAutoOrientation = 1f;
    [Tooltip("Degrees per second. How fast the target has to move for auto orient to activate.")]
    [SerializeField]
    private float _autoOrientActivationSpeed = 1f;
    [Tooltip("Target to track.")]
    [SerializeField]
    private Transform _target;

    private Transform _pivot;
    private Transform _camera;
    private float _lookAngle = 0f;
    private float _tiltAngle = 0f;
    private Quaternion _targetRotation;
    private Quaternion _targetPivotRotation;
    private Vector3 _pivotEulers;
    private Vector3 _targetPreviousPosition;
    private float _currentTiltVelocity;
    private float _lastInputTime = float.MinValue;

    private Vector2 _rotationAxis; //How much to rotate the camera

    //private bool _usingMouse = true;

    public void Init()
    {
        transform.SetParent(null);

        _targetPreviousPosition = _target.position;
        _camera = GetComponentInChildren<Camera>().transform;
        _pivot = _camera.parent;
        _pivotEulers = _pivot.rotation.eulerAngles;
        _tiltAngle = _targetTilt;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            HandleRotationMovement();
            FollowTarget();
        }
    }

    public void Rotate(Vector2 axis)
    {
        _rotationAxis = axis;
    }


    private void FollowTarget()
    {
        if (_target != null)
        {
            transform.position = _target.position;
            _targetPreviousPosition = _target.position;
        }
    }

    private void HandleRotationMovement()
    {
        // If input is not zero
        if (_rotationAxis != Vector2.zero)
        {
            // Apply input values to look and tilt.
            _lookAngle += _rotationAxis.x * _turnSpeed * Time.fixedDeltaTime;
            _tiltAngle += _rotationAxis.y * _turnSpeed * Time.fixedDeltaTime * (_invertYAxis ? -1 : 1);

            _lastInputTime = Time.time;

            _rotationAxis = Vector2.zero;
        }
        else if (ShouldAutoOrient())
            AutoOrient();

        _targetRotation = Quaternion.Euler(0f, _lookAngle, 0f);
        _tiltAngle = Mathf.Clamp(_tiltAngle, -_tiltMin, _tileMax);
        _targetPivotRotation = Quaternion.Euler(_tiltAngle, _pivotEulers.y, _pivotEulers.z);

        // Apply rotations
        transform.localRotation = _targetRotation;
        _pivot.localRotation = _targetPivotRotation;
    }
    private bool ShouldAutoOrient()
    {
        return Time.time - _lastInputTime >= _idleTimeBeforeAutoOrientation;
    }
    private void AutoOrient()
    {
        var playerMoveSpeed = Vector3.Distance(_target.position, _targetPreviousPosition) / Time.fixedDeltaTime;
        if (playerMoveSpeed > _autoOrientActivationSpeed)
        {
            AutoTurn();
            AutoTilt();
        }

    }
    private void AutoTurn()
    {
        var targetRot = Quaternion.Euler(0, _target.rotation.eulerAngles.y, 0);
        var pivotRot = Quaternion.Euler(0, _pivot.rotation.eulerAngles.y, 0);
        var dot = Vector3.Dot(targetRot * Vector3.right, pivotRot * Vector3.forward);

        if (Mathf.Abs(dot) >= 0.1f)
        {
            if (dot < 0) // Moving Right
            {
                _lookAngle += _autoTurnSpeed * Time.fixedDeltaTime;
            }
            else // Moving Left
            {
                _lookAngle -= _autoTurnSpeed * Time.fixedDeltaTime;
            }
        }
    }
    private void AutoTilt()
    {
        _tiltAngle = Mathf.SmoothDamp(_tiltAngle, _targetTilt, ref _currentTiltVelocity, _autoTiltTime);
    }
}