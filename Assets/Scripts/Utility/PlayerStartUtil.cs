#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class PlayerStartUtil
{
    [MenuItem("OP/Align Player Start to View &s", false, 1)]
    private static void AlignPlayerStartToView()
    {
        var view = SceneView.currentDrawingSceneView;
        if (view == null)
            view = SceneView.lastActiveSceneView;
        if (view != null)
        {
            var camera = view.camera;
            if (camera == null) return;
            var ray = new Ray(camera.transform.position, camera.transform.forward);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
                MovePlayerStart(hitInfo.point, camera.transform.rotation);
            else
                MovePlayerStart(camera.transform.position, camera.transform.rotation);
        }
    }

    private static void MovePlayerStart(Vector3 position, Quaternion rotation = default(Quaternion))
    {
        var playerStart = GameObject.FindObjectOfType<PlayerStart>();
        if (playerStart == null)
        {
            playerStart = PrefabUtility.InstantiatePrefab(Resources.Load<PlayerStart>("Player/P_PlayerStart")) as PlayerStart;
            Undo.RegisterCreatedObjectUndo(playerStart.gameObject, "Create Player Start");
        }
        else
            Undo.RecordObject(playerStart.gameObject, "Move Player Start");

        playerStart.transform.position = position;
    }
}
#endif