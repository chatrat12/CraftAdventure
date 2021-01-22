using UnityEngine;

[DefaultExecutionOrder(15)]
public class CameraClipProtection : MonoBehaviour
{
	[Tooltip("Time it take for camera to return to it's original distance after camera has been protected from clipping.")]
	public float ReturnTime = 0.4f;
	[Tooltip("Radius of the raycast shpere.")]
	public float SphereCastRadius = 0.1f;
	[Tooltip("Physics layers that should not be protected against.")]
	public LayerMask DontClipLayers;
	[Tooltip("Show debug info in the editor.")]
	public bool VisualizeInEditor = true;

	public bool Protecting { get; private set; }

	private Transform _camera;
	private Transform _pivot;
	private float _originalDistance;
	private float _currentDistance;
	private Ray _ray = new Ray();
	private RaycastHit[] _raycastHits;
	private float _moveVelocity;

	private void Awake()
	{
		_camera = GetComponentInChildren<Camera>().transform;
		_pivot = _camera.parent;
		_originalDistance = _camera.localPosition.magnitude;
		_currentDistance = _originalDistance;
	}

	private void LateUpdate()
	{
		float targetDistance = _originalDistance;
		_ray.origin = _pivot.position + _pivot.forward * SphereCastRadius;
		_ray.direction = -_pivot.forward;

		var invertedLayerMask = ~DontClipLayers.value;
		var collisions = Physics.OverlapSphere(_ray.origin, SphereCastRadius, invertedLayerMask);

		var initialCollsion = false;

		foreach (var collision in collisions)
		{
			if (!collision.isTrigger)
			{
				initialCollsion = true;
				break;
			}
		}

		if (initialCollsion)
		{
			_ray.origin += _pivot.forward * SphereCastRadius;
			_raycastHits = Physics.RaycastAll(_ray, _originalDistance - SphereCastRadius, invertedLayerMask);
		}
		else
		{
			_raycastHits = Physics.SphereCastAll(_ray, SphereCastRadius, _originalDistance + SphereCastRadius, invertedLayerMask);
		}

		bool hitSomething = false;
		float nearest = Mathf.Infinity;
		foreach (var hit in _raycastHits)
		{
			if (hit.distance < nearest && !hit.collider.isTrigger)
			{
				nearest = hit.distance;
				targetDistance = -_pivot.InverseTransformPoint(hit.point).z;
				hitSomething = true;
			}
		}

		var closestDistance = 0f;
		Protecting = hitSomething;
		_currentDistance = Mathf.SmoothDamp(_currentDistance, targetDistance, ref _moveVelocity, _currentDistance > targetDistance ? 0 : ReturnTime);
		_currentDistance = Mathf.Clamp(_currentDistance, closestDistance, _originalDistance);
		_camera.localPosition = -Vector3.forward * _currentDistance;
	}

	void OnDrawGizmos()
	{
		if (!Application.isPlaying && VisualizeInEditor) return;
		Color backupColor = Gizmos.color;
		Gizmos.color = Color.green;
		Gizmos.DrawLine(_pivot.position, _pivot.position - _pivot.forward * _currentDistance);
		var color = Color.yellow;
		color.a = 0.5f;
		Gizmos.color = color;
		Gizmos.DrawSphere(_pivot.position - _pivot.forward * _currentDistance, SphereCastRadius);
		Gizmos.color = backupColor;
	}
}
