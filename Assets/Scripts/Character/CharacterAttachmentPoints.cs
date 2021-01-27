using System.Collections.Generic;
using UnityEngine;

public class CharacterAttachmentPoints : MonoBehaviour
{
    public AttachmentPointEquipmentSheathable PrimaryWeapon { get; private set; }
    public BasicAttachmentPoint MainHandUtility { get; private set; }

    [SerializeField] private EquipmentAttachPointSettings[] _attachPointsSettings;

    private Dictionary<EquipmentSlotType, AttachmentPointEquipment> _equipment = new Dictionary<EquipmentSlotType, AttachmentPointEquipment>();
    private EquipmentModelCache _modelCache;

    public void Init(CharacterEquipment equipment, EquipmentModelCache modelCache)
    {
        _modelCache = modelCache;
        foreach(var attachSettings in _attachPointsSettings)
        {
            var slot = equipment.GetSlot(attachSettings.StotType);
            _equipment.Add(attachSettings.StotType, CreateAttachPointFromSettings(attachSettings, slot));
        }
        PrimaryWeapon = _equipment[EquipmentSlotType.PrimaryWeapon] as AttachmentPointEquipmentSheathable;
        MainHandUtility = CreateUtilTransform(PrimaryWeapon.UnsheathedTransform, "MainHandUtil");
    }

    private AttachmentPointEquipment CreateAttachPointFromSettings(EquipmentAttachPointSettings settings, EquipmentSlot slot)
    {
        if(settings.Sheathable)
            return new AttachmentPointEquipmentSheathable(settings.StotType.ToString(), slot, _modelCache, settings.SheathedAttachPoint, settings.AttachPoint);
        return new AttachmentPointEquipment(settings.AttachPoint, slot, _modelCache);
    }

    private BasicAttachmentPoint CreateUtilTransform(Transform mainHand, string name)
    {
        var utilTransform = new GameObject(name).transform;
        utilTransform.SetParent(mainHand.parent);
        utilTransform.localPosition = mainHand.localPosition;
        utilTransform.rotation = mainHand.rotation;
        return new BasicAttachmentPoint(utilTransform);
    }

    public AttachmentPointEquipment GetEquipmentPoint(EquipmentSlotType slotType)
    {
        if (_equipment.ContainsKey(slotType))
            return _equipment[slotType];
        return null;
    }
}