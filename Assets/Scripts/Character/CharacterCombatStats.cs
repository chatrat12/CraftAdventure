public class CharacterCombatStats
{
    public CombatStats BaseStats => _baseStats;

    private CombatStats _baseStats;


    public CharacterCombatStats(CharacterInfo info)
    {
        _baseStats = info.BaseStats;
    }
}
