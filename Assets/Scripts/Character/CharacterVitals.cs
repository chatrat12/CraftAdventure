
public class CharacterVitals
{
    public float MaxHealth => _character.CombatStats.BaseStats.HealthPool;
    public float Health { get; private set; }

    private Character _character;

    public CharacterVitals(Character character)
    {
        _character = character;
        Health = MaxHealth;
    }
}
