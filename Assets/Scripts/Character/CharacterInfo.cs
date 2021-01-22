using UnityEngine;

[CreateAssetMenu(menuName = "CA/Character Info")]
public class CharacterInfo : IndentifiableScriptableObject
{
    public string Name => _name;
    public CombatStats BaseStats => _baseStats;

    [SerializeField] private string _name;
    [SerializeField] private CombatStats _baseStats;
}