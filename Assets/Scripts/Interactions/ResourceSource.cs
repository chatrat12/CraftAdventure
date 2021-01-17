using UnityEngine;

public class ResourceSource : InteractableObject
{
    [SerializeField] ItemStack _item;
    [SerializeField] Transform _model;
    [SerializeField] Transform _depletedOverride;

    public override string InteractMessage => base.InteractMessage;
    public bool Depleted { get; private set; }

    public override bool CanInteract(Player player) => !Depleted;

    public override void Interact(Player player)
    {
        player.Inventory.AddItem(_item);

        Depleted = true;
        _model.gameObject.SetActive(false);
        if (_depletedOverride != null)
            _depletedOverride.gameObject.SetActive(true);
    }
}
