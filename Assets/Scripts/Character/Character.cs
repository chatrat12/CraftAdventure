using System;
using UnityEngine;

public class Character
{
    public string Name => _info.Name;
    public CharacterAvatar Avatar { get; private set; }
    public CharacterCombatStats CombatStats { get; private set; }
    public CharacterVitals Vitals { get; private set; }
    public Damagable Damage { get; private set; }

    protected CharacterInfo _info;

    public Character(CharacterAvatar avatar, CharacterInfo info)
    {
        Avatar = avatar;
        _info = info;
        CombatStats = new CharacterCombatStats(info);
        Vitals = new CharacterVitals(this);
        Damage = new Damagable();

        Damage.TookDamage += (sender, dmg, cause, type) =>
        {
            Debug.Log($"{Name} tool {dmg} {type} damage from {cause}");
        };
    }

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void OnAnimatorMove() { }
#if UNITY_EDITOR
    public virtual void OnDrawGizmos() { }
#endif
}
