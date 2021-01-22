using System.Linq;
using UnityEngine;

public class PlayerInteractDetection
{
    public InteractableObject AvailableInteraction { get; private set; }

    private Collider[] _buffer = new Collider[10];
    private Player _player;

    public PlayerInteractDetection(Player player)
    {
        _player = player;
    }

    public void Update()
    {
        var pos = Vector3.forward * 0.5f + Vector3.up * 0.25f;
        pos = _player.Avatar.transform.localToWorldMatrix.MultiplyPoint(pos);
        var count = Physics.OverlapSphereNonAlloc(pos, 0.5f, _buffer);

        AvailableInteraction = _buffer.Where((o, i) => i < count)
                                       .Select(o => o.GetComponent<InteractableObject>())
                                       .Where(io => io != null && io.CanInteract(_player))
                                       .FirstOrDefault();
    }
    
    public void OnDrawGizmos()
    {
        var pos = Vector3.forward * 0.5f + Vector3.up * 0.25f;
        pos = _player.Avatar.transform.localToWorldMatrix.MultiplyPoint(pos);
        //Gizmos.DrawWireSphere(pos, 0.5f);
    }
}