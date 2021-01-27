using UnityEngine;

public class OverlapDetectorCapsule : OverlapDetector
{
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private float _height = 1f;

    protected override void UpdateBuffer()
    {
        var pos = transform.localToWorldMatrix.MultiplyPoint3x4(_offset);
        var point1 = pos + transform.up * (_height * 0.5f);
        var point2 = pos + -transform.up * (_height * 0.5f);
        _bufferCount = Physics.OverlapCapsuleNonAlloc(point1, point2, _radius, _buffer,
                                                      _layerMaskSettings.LayerMask,
                                                      _layerMaskSettings.QueryTriggerInteraction);
    }

#if UNITY_EDITOR
    protected override void DrawGizmo()
    {
        var point1 = Vector3.up * (_height * 0.5f);
        var point2 = -Vector3.up * (_height * 0.5f);
        Gizmos.DrawWireSphere(point1, _radius);
        Gizmos.DrawWireSphere(point2, _radius);

        void DrawLine(Vector3 dir)
            => Gizmos.DrawLine(point1 + dir * _radius, point2 + dir * _radius);

        DrawLine(Vector3.forward);
        DrawLine(-Vector3.forward);
        DrawLine(Vector3.right);
        DrawLine(-Vector3.right);
    }
#endif
}
