public class CharacterEquipmentManagement
{
    public CharacterAttachmentPoints AttachmentPoints { get; set; }
    public EquipmentModelCache ModelCache { get; private set; }
    public CharacterEquipment Items { get; private set; }

    private Character _character;
    public CharacterEquipmentManagement(Character character)
    {
        _character = character;

        AttachmentPoints = character.Avatar.GetComponent<CharacterAttachmentPoints>();
        ModelCache = new EquipmentModelCache();
        Items = new CharacterEquipment();
        Items.AddEquipmentSlot(EquipmentSlotType.PrimaryWeapon);
        Items.AddEquipmentSlot(EquipmentSlotType.SecondayWeapon);

        AttachmentPoints.Init(Items, ModelCache);
    }
}
