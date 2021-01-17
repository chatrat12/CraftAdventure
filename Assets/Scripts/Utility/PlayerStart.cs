using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    private void Awake()
    {
        Initialization.Initialize(transform.position);
        Destroy(this.gameObject);
    }
}
