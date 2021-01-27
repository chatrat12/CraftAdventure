using UnityEngine;

public abstract class ItemEquipment : Item
{
    public EquipmentComponent Model => _model;
    public CombatStats StatModifier => _statModifier;
    public EquipmentSlotType SlotType => _slotType;

    [SerializeField] private CombatStats _statModifier;
    [SerializeField] private EquipmentComponent _model;
    [SerializeField] private EquipmentSlotType _slotType;

    public virtual bool CanEquip(Character character)
    {
        return true; //_usableBy.ContainsHero(heroID);
    }

    protected override void BuildTooltipStats(UITooltip tooltip)
    {
        tooltip.AddLine(SlotType.ToString()); //UITooltipColors.SecretUpgradeColor);
        foreach (var statPair in _statModifier)
            tooltip.AddLine(string.Format("+{0} {1}", statPair.Value, statPair.Name));
    }
}