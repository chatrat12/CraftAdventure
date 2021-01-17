using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public virtual string InteractMessage => "Interact";
    public abstract void Interact(Player player);
    public virtual bool CanInteract(Player player) => true;
}