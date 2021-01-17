using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Player Player { get; set; }
    private void LateUpdate()
    {
        if (Player != null)
            transform.position = Player.Avatar.transform.position;
    }
}
