using System.Collections.Generic;
using UnityEngine;

public static class Damage
{
    private static Collider[] _overlapResults = new Collider[50];
    private static List<IDamagable> _damagablesDamaged = new List<IDamagable>();

    public static void ApplyGenericDamage(GameObject target, float baseDamage, GameObject damageCauser = null)
    {
        var damagable = target.GetComponentInParent<IDamagable>();
        if (damagable != null)
            damagable.Damage.TakeGenericDamage(baseDamage, damageCauser);
    }

    public static void ApplyPointDamage(GameObject target, float baseDamage, GameObject damageCauser = null, Vector3 hitDirection = default(Vector3), float force = 0, RaycastHit? hitInfo = null)
    {
        var damagable = target.GetComponentInParent<IDamagable>();
        if (damagable != null)
            damagable.Damage.TakePointDamage(baseDamage, damageCauser, hitDirection, force, hitInfo);
    }

    public static void ApplyRadialDamage(float baseDamage, Vector3 origin, float radius, LayerMask layers, GameObject damageCauser = null, bool ignoreCauser = true, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal, float force = 0f)
    {
        int numOfOverlaps = Physics.OverlapSphereNonAlloc(origin, radius, _overlapResults, layers, triggerInteraction);
        ApplyRadialDamageToOverlaps(numOfOverlaps, baseDamage, damageCauser, ignoreCauser, origin, radius, force);
    }

    public static void ApplyRadialDamage(float baseDamage, Vector3 origin, float radius, GameObject damageCauser = null, bool ignoreCauser = true, float force = 0f)
    {
        int numOfOverlaps = Physics.OverlapSphereNonAlloc(origin, radius, _overlapResults);
        ApplyRadialDamageToOverlaps(numOfOverlaps, baseDamage, damageCauser, ignoreCauser, origin, radius, force);
    }

    private static void ApplyRadialDamageToOverlaps(int numOfOverlaps, float baseDamage, GameObject damageCauser, bool ignoreCauser, Vector3 origin, float radius, float force)
    {
#if DEBUG
        if (numOfOverlaps > _overlapResults.Length)
            Debug.LogError("Number of results is larger than provided array!");
#endif
        var causerDamagable = damageCauser != null ? damageCauser.GetComponentInParent<IDamagable>() : null;
        IDamagable damagable;
        for (int i = 0; i < numOfOverlaps; i++)
        {
            damagable = _overlapResults[i].gameObject.GetComponentInParent<IDamagable>();

            if (damagable != null && damagable != causerDamagable && !_damagablesDamaged.Contains(damagable))
            {
                _damagablesDamaged.Add(damagable);
                damagable.Damage.TakeRadialDamage(baseDamage, damageCauser, origin, radius, force);
            }
        }
        _damagablesDamaged.Clear();
    }
}