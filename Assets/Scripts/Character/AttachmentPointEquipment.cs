using UnityEngine;

public class AttachmentPointEquipment : BasicAttachmentPoint
{
    private EquipmentModelCache _modelCache;
    private EquipmentSlot _slot;

    public AttachmentPointEquipment(Transform root, EquipmentSlot slot, EquipmentModelCache modelCache) : base(root)
    {
        _modelCache = modelCache;
        _slot = slot;

        _slot.OnEquipmentChanged.AddListener((type, newItem, oldItem) =>
        {
            Clear();
            if (newItem != null)
                Attach(_modelCache.GetModel(newItem).transform);
        });
    }
}