#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneListEditorWindow : EditorWindow
{
    [SerializeField]
    private static Vector2 _scrollPos = Vector2.zero;
    private (string name, string path)[] _entries;


    [MenuItem("CA/Open Scene List")]
    private static void Open()
    {
        var window = GetWindow<SceneListEditorWindow>();
        window.titleContent = new GUIContent("Scenes");
        window.Show();
    }

    private void OnEnable()
    {
        string[] guids = AssetDatabase.FindAssets("t:scene", null);
        _entries = guids.Select(g =>
        {
            var path = AssetDatabase.GUIDToAssetPath(g);
            var name = Path.GetFileNameWithoutExtension(path);
            return (name, path);
        }).ToArray();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        _scrollPos = GUILayout.BeginScrollView(_scrollPos);

        foreach (var entry in _entries)
            DrawMiscSceneEntry(entry.name, entry.path);

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }


    private void DrawMiscSceneEntry(string name, string path)
    {
        GUILayout.BeginHorizontal("box");
        GUILayout.Label(name);
        GUILayout.FlexibleSpace();
        if (!EditorApplication.isPlaying)
        {
            if (GUILayout.Button("Open"))
            {
                var scenes = new Scene[EditorSceneManager.sceneCount];
                for (int i = 0; i < scenes.Length; i++)
                    scenes[i] = EditorSceneManager.GetSceneAt(i);
                if (EditorSceneManager.SaveModifiedScenesIfUserWantsTo(scenes))
                    EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            }
            if (GUILayout.Button("Add"))
                EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
        }
        GUILayout.EndHorizontal();
    }


}
#endif