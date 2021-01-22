#if DEBUG
using UnityEngine;

public class DebugTimeScale : MonoBehaviour
{
    private const float SPEED_UP_MULTIPLIER = 5f;
    private const KeyCode SPEED_UP_KEY = KeyCode.Q;

    [RuntimeInitializeOnLoadMethod]
    private static void CreateInstance()
    {
        var go = new GameObject("DebugInput");
        go.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
        DontDestroyOnLoad(go);
        go.AddComponent<DebugTimeScale>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(SPEED_UP_KEY) || Input.GetKeyDown("joystick button 6")) Time.timeScale = SPEED_UP_MULTIPLIER;
        if (Input.GetKeyUp(SPEED_UP_KEY) || Input.GetKeyUp("joystick button 6")) Time.timeScale = 1;
    }
}
#endif