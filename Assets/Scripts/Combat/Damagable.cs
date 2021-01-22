using UnityEngine;

public class Damagable
{
    public delegate void TakeDamageEvent(Damagable sender, float baseDamage, GameObject damageCauser, DamageType damageType);
    public delegate void TakePointDamageEvent(Damagable sender, float baseDamage, GameObject damageCauser, Vector3 hitDirection, float force, RaycastHit? hitInfo);
    public delegate void TakeRadialDamageEvent(Damagable sender, float baseDamage, GameObject damageCauser, Vector3 origin, float radius, float force);
    public delegate void HealEvent(Damagable sender, float baseHealAmount, GameObject healer);

    public event TakeDamageEvent TookDamage;
    public event TakePointDamageEvent TookPointDamage;
    public event TakeRadialDamageEvent TookRadialDamage;
    public event HealEvent Healed;


    public void TakeGenericDamage(float baseDamage, GameObject damageCauser = null)
    {
        OnTookDamage(baseDamage, damageCauser, DamageType.Generic);
    }

    public void TakePointDamage(float baseDamage, GameObject damageCauser = null, Vector3 hitDirection = default(Vector3), float force = 0f, RaycastHit? hitInfo = null)
    {
        OnTookPointDamage(baseDamage, damageCauser, hitDirection, force, hitInfo);
    }

    public void TakeRadialDamage(float baseDamage, GameObject damageCauser = null, Vector3 origin = default(Vector3), float radius = 0f, float force = 0f)
    {
        OnTookRadialDamage(baseDamage, damageCauser, origin, radius, force);
    }
    public void Heal(float baseHealAmount, GameObject healer = null)
    {
        OnHealed(baseHealAmount, healer);
    }

    protected virtual void OnTookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if (TookDamage != null)
            TookDamage(this, baseDamage, damageCauser, DamageType.Generic);
    }
    protected virtual void OnTookPointDamage(float baseDamage, GameObject damageCauser, Vector3 hitDirection, float force, RaycastHit? hitInfo)
    {

        OnTookDamage(baseDamage, damageCauser, DamageType.Point);
        if (TookPointDamage != null)
            TookPointDamage(this, baseDamage, damageCauser, hitDirection, force, hitInfo);
    }
    protected virtual void OnTookRadialDamage(float baseDamage, GameObject damageCauser, Vector3 origin, float radius, float force)
    {
        OnTookDamage(baseDamage, damageCauser, DamageType.Point);
        if (TookRadialDamage != null)
            TookRadialDamage(this, baseDamage, damageCauser, origin, radius, force);
    }
    protected virtual void OnHealed(float baseHealAmount, GameObject healer)
    {
        if (Healed != null)
            Healed(this, baseHealAmount, healer);
    }
}

public enum DamageType
{
    Generic,
    Point,
    Radial
}
