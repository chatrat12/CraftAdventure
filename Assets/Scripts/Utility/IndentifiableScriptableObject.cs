using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class IndentifiableScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    const ulong ASSET_FILE_ID = 11400000;


    public int ID => _id;

    [HideInInspector]
    [SerializeField] private int _id;

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        var gID = GlobalObjectId.GetGlobalObjectIdSlow(this);
        var newID = gID.targetObjectId == ASSET_FILE_ID ? gID.assetGUID.GetHashCode() : (int)gID.targetObjectId;

        if (_id != newID)
        {
            _id = newID;
            EditorUtility.SetDirty(this);
        }
#endif
    }
    public void OnAfterDeserialize() { }

#if UNITY_EDITOR
    [CustomEditor(typeof(IndentifiableScriptableObject))]
    public class GuidSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var id = serializedObject.FindProperty("_id").intValue;
            EditorGUILayout.LabelField($"ID: {id}");
            base.OnInspectorGUI();
        }
    }
#endif
}