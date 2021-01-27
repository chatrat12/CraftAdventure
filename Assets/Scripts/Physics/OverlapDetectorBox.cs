using UnityEngine;

public class OverlapDetectorBox : OverlapDetector
{
    [SerializeField] private Vector3 _extents = Vector3.zero * 0.5f;

    protected override void UpdateBuffer()
    {
        var pos = transform.localToWorldMatrix.MultiplyPoint3x4(_offset);
        _bufferCount = Physics.OverlapBoxNonAlloc(pos, _extents, _buffer, transform.rotation, 
                                                  _layerMaskSettings.LayerMask, 
                                                  _layerMaskSettings.QueryTriggerInteraction);
    }

#if UNITY_EDITOR
    protected override void DrawGizmo()
    {
        Gizmos.DrawWireCube(_offset, _extents * 2);
    }
#endif
}
