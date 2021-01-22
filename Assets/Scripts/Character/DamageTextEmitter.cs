using UnityEngine;

public class DamageTextEmitter : MonoBehaviour
{
    [SerializeField]
    private float _emissionRadius = 0.5f;

    private void Awake()
    {
        var damageable = GetComponentInParent<IDamagable>();
        if (damageable != null)
            damageable.Damage.TookDamage += Damageable_TookDamage;
    }

    private void Damageable_TookDamage(Damagable sender, float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        var pos = transform.position + Random.insideUnitSphere * _emissionRadius;
        HitText.Create(baseDamage.ToString(), pos, Vector3.up, Color.white);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _emissionRadius);
        Gizmos.color = Color.white;
    }
#endif
}