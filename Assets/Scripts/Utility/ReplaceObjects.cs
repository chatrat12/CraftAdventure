#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ReplaceObjects : EditorWindow
{
    [SerializeField] private GameObject _newObject;
    [SerializeField] private bool _keepName = false;

    [MenuItem("CA/Utils/Replace Objects")]
    private static void OpenWindow()
    {
        GetWindow<ReplaceObjects>();
    }

    private void OnEnable()
    {
        titleContent.text = "Replace Objects";
    }

    private void OnGUI()
    {
        _newObject = EditorGUILayout.ObjectField("New Object", _newObject, typeof(GameObject), true) as GameObject;
        _keepName = EditorGUILayout.Toggle("Keep Name", _keepName);
        if (GUILayout.Button("Replace"))
        {
            if (_newObject == null) return;
            ReplaceItems(Selection.gameObjects.Select(go => go.transform), _newObject, _keepName);
        }
    }

    private void ReplaceItems(IEnumerable<Transform> objectsToReplace, GameObject replacement, bool keepName)
    {
        foreach (var obj in objectsToReplace)
        {
            if (AssetDatabase.Contains(obj.gameObject)) continue;
            GameObject newObject = CreateObjectInstance(replacement);
            Undo.RegisterCreatedObjectUndo(newObject, "Replace Objects");
            if (keepName)
                newObject.name = obj.name;
            newObject.transform.parent = obj.parent;
            newObject.transform.localPosition = obj.localPosition;
            newObject.transform.localRotation = obj.localRotation;
            newObject.transform.localScale = obj.localScale;
            newObject.transform.SetSiblingIndex(obj.GetSiblingIndex());
            while (obj.childCount > 0)
            {
                obj.GetChild(0).SetParent(newObject.transform);
            }
            Undo.DestroyObjectImmediate(obj.gameObject);
        }
    }

    private GameObject CreateObjectInstance(GameObject newObject)
    {
        if (PrefabUtility.GetPrefabAssetType(newObject) != PrefabAssetType.NotAPrefab)
            return PrefabUtility.InstantiatePrefab(newObject) as GameObject;
        else
            return GameObject.Instantiate(newObject);
    }
}
#endif