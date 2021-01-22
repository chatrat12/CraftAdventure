using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ResistStats
{
    public float Fire { get { return _fire; } set { _fire = value; } }
    public float Earth { get { return _earth; } set { _earth = value; } }
    public float Water { get { return _water; } set { _water = value; } }
    public float Shock { get { return _shock; } set { _shock = value; } }

    public float Accumulative => GetAccumulative();

    [SerializeField] private float _fire;
    [SerializeField] private float _earth;
    [SerializeField] private float _water;
    [SerializeField] private float _shock;

    public ResistStats(float fire, float earth, float water, float shock)
    {
        _fire = fire;
        _earth = earth;
        _water = water;
        _shock = shock;
    }

    private float GetAccumulative() => _fire + _earth + _water + _shock;

    public static ResistStats operator +(ResistStats a, ResistStats b)
    {
        return new ResistStats
        (
            a._fire + b._fire,
            a._earth + b._earth,
            a._water + b._water,
            a._shock + b._shock
        );
    }

    public float GetResistance(ElementalDamageType damageType)
    {
        switch (damageType)
        {
            case ElementalDamageType.Fire: return _fire;
            case ElementalDamageType.Earth: return _earth;
            case ElementalDamageType.Water: return _water;
            case ElementalDamageType.Shock: return _shock;
        }
        return 0;
    }

    public static ResistStats operator -(ResistStats a, ResistStats b)
    {
        return new ResistStats
        (
            a._fire - b._fire,
            a._earth - b._earth,
            a._water - b._water,
            a._shock - b._shock
        );
    }

    public static ResistStats operator *(ResistStats a, ResistStats b)
    {
        return new ResistStats
        (
            a._fire * b._fire,
            a._earth * b._earth,
            a._water * b._water,
            a._shock * b._shock
        );
    }

    public static bool operator ==(ResistStats a, ResistStats b) => ResistStats.Equals(a, b);
    public static bool operator !=(ResistStats a, ResistStats b) => !ResistStats.Equals(a, b);

    public override bool Equals(object obj)
    {
        if (!(obj is ResistStats))
            return false;

        ResistStats other = (ResistStats)obj;

        return _fire.Equals(other._fire) &&
               _earth.Equals(other._earth) &&
               _water.Equals(other._water) &&
               _shock.Equals(other._shock);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 7;
            hash = hash * 11 + _fire.GetHashCode();
            hash = hash * 11 + _earth.GetHashCode();
            hash = hash * 11 + _water.GetHashCode();
            hash = hash * 11 + _shock.GetHashCode();
            return hash;
        }
    }

    public IEnumerable<CombatStats.StatPair> GetStats()
    {
        if (_fire != 0)
            yield return new CombatStats.StatPair("FIRE RES", _fire);
        if (_earth != 0)
            yield return new CombatStats.StatPair("EARTH RES", _earth);
        if (_water != 0)
            yield return new CombatStats.StatPair("WATER RES", _water);
        if (_shock != 0)
            yield return new CombatStats.StatPair("SHOCK RES", _shock);
    }
}
