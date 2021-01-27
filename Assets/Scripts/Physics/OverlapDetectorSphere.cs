using UnityEngine;

public class OverlapDetectorSphere : OverlapDetector
{
    [SerializeField] private float _radius = 0.5f;

    protected override void UpdateBuffer()
    {
        var pos = transform.localToWorldMatrix.MultiplyPoint3x4(_offset);
        _bufferCount = Physics.OverlapSphereNonAlloc(pos, _radius, _buffer,
                                                     _layerMaskSettings.LayerMask,
                                                     _layerMaskSettings.QueryTriggerInteraction);
    }

#if UNITY_EDITOR
    protected override void DrawGizmo()
    {
        Gizmos.DrawWireSphere(_offset, _radius);
    }
#endif
}
