using UnityEngine;
using UnityEngine.InputSystem;

public class DamageTest : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hitInfo))
            {
                var dmg = 10;
                Damage.ApplyPointDamage(hitInfo.collider.gameObject, dmg, null, ray.direction, 100, hitInfo);
            }
        }
    }
}
