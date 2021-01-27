using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

[System.Serializable]
public class EquipmentAttachPointSettings
{
    public EquipmentSlotType StotType => _slotType;
    public Transform AttachPoint => _attachPoint;
    public bool Sheathable => _sheathable;
    public Transform SheathedAttachPoint => _sheathedAttachPoint;

    [SerializeField] private EquipmentSlotType _slotType;
    [SerializeField] private Transform _attachPoint;
    [SerializeField] private bool _sheathable = false;
    [SerializeField] private Transform _sheathedAttachPoint;
}

#if UNITY_EDITOR
//[CustomPropertyDrawer(typeof(EquipmentAttachPointSettings))]
//public class EquipmentAttachPointSettingsPropertyDrawer : PropertyDrawer
//{
//    public override VisualElement CreatePropertyGUI(SerializedProperty property)
//    {
//        var container = new VisualElement();

//        //var slotType = new PropertyField(property.FindPropertyRelative("_slotType")) { label = " " };
//        //var attachPoint = new PropertyField(property.FindPropertyRelative("_attachPoint")) { label = " " };
//        //var sheathable = new PropertyField(property.FindPropertyRelative("_sheathable"));
//        //var sheathAttachPoint = new PropertyField(property.FindPropertyRelative("_sheathedAttachPoint"));
//        //sheathAttachPoint.style.marginRight = 20;

//        //var horizontal = new VisualElement();
//        //horizontal.style.flexDirection = FlexDirection.Row;

//        //horizontal.Add(slotType);
//        //horizontal.Add(attachPoint);

//        //container.Add(horizontal);
//        //container.Add(sheathable);
//        //container.Add(sheathAttachPoint);

//        return container;
//    }
//}
#endif