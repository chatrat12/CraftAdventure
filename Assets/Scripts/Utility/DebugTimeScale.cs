#if DEBUG
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTimeScale : MonoBehaviour
{
    private const float SPEED_UP_MULTIPLIER = 5f;
    private const Key SPEED_UP_KEY = Key.Q;

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
        if (Keyboard.current[SPEED_UP_KEY].wasPressedThisFrame) Time.timeScale = SPEED_UP_MULTIPLIER;
        if (Keyboard.current[SPEED_UP_KEY].wasReleasedThisFrame) Time.timeScale = 1;
    }
}
#endif