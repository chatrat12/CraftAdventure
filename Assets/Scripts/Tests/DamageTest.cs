using UnityEngine;

public class DamageTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                var dmg = 10;
                Damage.ApplyPointDamage(hitInfo.collider.gameObject, dmg, null, ray.direction, 100, hitInfo);
            }
        }
    }
}
