using UnityEngine;

public class AttachmentPointEquipmentSheathable : AttachmentPointEquipment
{
    public bool Sheathed
    {
        get => _sheathed;
        set
        {
            _sheathed = value;
            ResetTransform(value ? SheathedTransform : UnsheathedTransform);
        }
    }
    public Transform SheathedTransform { get; private set; }
    public Transform UnsheathedTransform { get; private set; }

    private bool _sheathed = true;

    public AttachmentPointEquipmentSheathable(string transformName, EquipmentSlot slot, EquipmentModelCache modelCache, Transform sheathedTransfrom, Transform unsheathedTransform) : base(new GameObject(transformName).transform, slot, modelCache)
    {
        SheathedTransform = sheathedTransfrom;
        UnsheathedTransform = unsheathedTransform;
        ResetTransform(sheathedTransfrom);
    }

    private void ResetTransform(Transform parent)
        => Root.SetParent(parent, false);
}
