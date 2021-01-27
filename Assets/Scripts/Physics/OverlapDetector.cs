using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class OverlapDetector : MonoBehaviour
{
    public OperatingMode Mode
    {
        get => _mode;
        set => _mode = value;
    }

    public IEnumerable<Collider> Overlaps
    {
        get
        {
            if (_mode == OperatingMode.Explicit)
                UpdateBuffer();
            return GetOverlapEnumerable();
        }
    }

    [SerializeField] private int _bufferSize = 10;
    [SerializeField] private OperatingMode _mode = OperatingMode.Auto;
    [SerializeField] protected PhysicsLayerSettings _layerMaskSettings;
    [Header("Shape")]
    [SerializeField] protected Vector3 _offset;

    protected int _bufferCount = 0;
    protected Collider[] _buffer;

    private void Awake()
    {
        _buffer = new Collider[_bufferSize];
    }

    private void Update()
    {
        if (_mode == OperatingMode.Auto)
            UpdateBuffer();
    }

    protected abstract void UpdateBuffer();

    private IEnumerable<Collider> GetOverlapEnumerable()
    {
        for (int i = 0; i < _bufferCount; i++)
            yield return _buffer[i];
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        if(UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.color = Overlaps.Any() ? Color.red : Color.cyan;
        }

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        DrawGizmo();
        Gizmos.color = Color.white;
        Gizmos.matrix = Matrix4x4.identity;
    }

    protected abstract void DrawGizmo();
#endif

    public enum OverlapShape
    {
        Box,
        Sphere,
        Capsule,
    }

    public enum OperatingMode
    {
        Auto,
        Explicit
    }
}
