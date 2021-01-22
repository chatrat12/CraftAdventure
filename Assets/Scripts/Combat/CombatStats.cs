using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CombatStats : IEnumerable<CombatStats.StatPair>
{
    public float Attack { get => _attack; set => _attack = value; }
    public float Defense { get => _defense; set => _defense = value; }
    public float MagicAttack { get => _magicAttack; set => _magicAttack = value; }
    public float MagicDefense { get => _magicDefense; set => _magicDefense = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float Evade { get => _evade; set => _evade = value; }
    public float Luck { get => _luck; set => _luck = value; }
    public float HealthPool { get => _healthPool; set => _healthPool = value; }
    public float Accuracy { get => _accuracy; set => _accuracy = value; }
    public ResistStats Resistances { get => _resistances; set => _resistances = value; }

    public float Accumulative => GetAccumulative();

    public static CombatStats Empty => new CombatStats();

    [SerializeField] private float _attack;
    [SerializeField] private float _defense;
    [SerializeField] private float _magicAttack;
    [SerializeField] private float _magicDefense;
    [SerializeField] private float _speed;
    [SerializeField] private float _evade;
    [SerializeField] private float _luck;
    [SerializeField] private float _healthPool;
    [SerializeField] private float _accuracy;
    [SerializeField] private ResistStats _resistances;

    public CombatStats(float attack,
                       float defense,
                       float magicAttack,
                       float magicDefense,
                       float speed,
                       float evade,
                       float luck,
                       float healthPool,
                       float accuracy,
                       ResistStats resistances)
    {
        _attack = attack;
        _defense = defense;
        _magicAttack = magicAttack;
        _magicDefense = magicDefense;
        _speed = speed;
        _evade = evade;
        _luck = luck;
        _healthPool = healthPool;
        _accuracy = accuracy;
        _resistances = resistances;
    }

    private float GetAccumulative()
        => _attack +
           _defense +
           _magicAttack +
           _magicDefense +
           _speed +
           _evade +
           _luck +
           _healthPool +
           _accuracy +
           _resistances.Accumulative;

    public static CombatStats operator +(CombatStats a, CombatStats b)
    {
        return new CombatStats
        (
            a._attack + b.Attack,
            a._defense + b._defense,
            a._magicAttack + b._magicAttack,
            a._magicDefense + b._magicDefense,
            a._speed + b._speed,
            a._evade + b._evade,
            a._luck + b._luck,
            a._healthPool + b._healthPool,
            a._accuracy + b._accuracy,
            a._resistances + b._resistances
        );
    }
    public static CombatStats operator -(CombatStats a, CombatStats b)
    {
        return new CombatStats
        (
            a._attack - b.Attack,
            a._defense - b._defense,
            a._magicAttack - b._magicAttack,
            a._magicDefense - b._magicDefense,
            a._speed - b._speed,
            a._evade - b._evade,
            a._luck - b._luck,
            a._healthPool - b._healthPool,
            a._accuracy - a._accuracy,
            a._resistances - b._resistances
        );
    }
    public static CombatStats operator *(CombatStats a, CombatStats b)
    {
        return new CombatStats
        (
            a._attack * b.Attack,
            a._defense * b._defense,
            a._magicAttack * b._magicAttack,
            a._magicDefense * b._magicDefense,
            a._speed * b._speed,
            a._evade * b._evade,
            a._luck * b._luck,
            a._healthPool * b._healthPool,
            a._accuracy * b._accuracy,
            a._resistances * b._resistances
        );
    }
    public static bool operator ==(CombatStats a, CombatStats b) => CombatStats.Equals(a, b);
    public static bool operator !=(CombatStats a, CombatStats b) => !CombatStats.Equals(a, b);

    public override bool Equals(object obj)
    {
        if (!(obj is CombatStats))
            return false;

        CombatStats other = (CombatStats)obj;

        return _attack.Equals(other._attack) &&
               _defense.Equals(other._defense) &&
               _magicAttack.Equals(other._magicAttack) &&
               _magicDefense.Equals(other._magicDefense) &&
               _speed.Equals(other._speed) &&
               _evade.Equals(other._evade) &&
               _luck.Equals(other._luck) &&
               _healthPool.Equals(other._healthPool) &&
               _accuracy.Equals(other._accuracy) &&
               _resistances.Equals(other._resistances);

    }
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 7;
            hash = hash * 11 + _attack.GetHashCode();
            hash = hash * 11 + _defense.GetHashCode();
            hash = hash * 11 + _magicAttack.GetHashCode();
            hash = hash * 11 + _magicDefense.GetHashCode();
            hash = hash * 11 + _speed.GetHashCode();
            hash = hash * 11 + _evade.GetHashCode();
            hash = hash * 11 + _luck.GetHashCode();
            hash = hash * 11 + _healthPool.GetHashCode();
            hash = hash * 11 + _accuracy.GetHashCode();
            hash = hash * 11 + _resistances.GetHashCode();
            return hash;
        }
    }

    public IEnumerator<StatPair> GetEnumerator()
    {
        if (_attack != 0)
            yield return new StatPair("ATT", _attack);
        if (_defense != 0)
            yield return new StatPair("DEF", _defense);
        if (_magicAttack != 0)
            yield return new StatPair("MAG", _magicAttack);
        if (_magicDefense != 0)
            yield return new StatPair("MAG DEF", _magicDefense);
        if (_speed != 0)
            yield return new StatPair("SPD", _speed);
        if (_evade != 0)
            yield return new StatPair("EVADE", _evade);
        if (_luck != 0)
            yield return new StatPair("LUCK", _luck);
        if (_healthPool != 0)
            yield return new StatPair("HP", _healthPool);
        if (_accuracy != 0)
            yield return new StatPair("ACC", _accuracy * 100);
        foreach (var pair in _resistances.GetStats())
            yield return pair;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct StatPair
    {
        public string Name { get; set; }
        public float Value { get; set; }

        public StatPair(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }
}