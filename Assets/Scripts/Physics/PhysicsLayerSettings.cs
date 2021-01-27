using UnityEngine;

[CreateAssetMenu(menuName = "CA/Physics/Physics Layer Settings")]
public class PhysicsLayerSettings : ScriptableObject
{
    public LayerMask LayerMask => _layerMask;
    public QueryTriggerInteraction QueryTriggerInteraction => _queryTriggerInteraction;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private QueryTriggerInteraction _queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
}
