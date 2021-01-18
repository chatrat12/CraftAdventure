using System.Linq;
using UnityAsync;
using UnityEngine;

public class ResourceSource : InteractableObject
{
    [SerializeField] ItemStack _item;
    [SerializeField] Transform _model;
    [SerializeField] Transform _depletedOverride;
    [SerializeField] AudioClip _harvestSound;
    [SerializeField] string _interactMessage = "Interact";
    [SerializeField] RequiredToolType _requiredTool = RequiredToolType.None;

    private AudioSource _source;

    public override string InteractMessage => _interactMessage;
    public bool Depleted { get; private set; }

    public override bool CanInteract(Player player) => !Depleted && CanHarvest(player);

    private void Awake()
    {
        CreateAudioSource();
    }

    public override async void Interact(Player player)
    {
        if (_requiredTool == RequiredToolType.Axe)
            player.Animation.ChopWood();
        else
            player.Animation.PickupAnimation();
        Depleted = true;
        await Await.Seconds(0.25f);

        player.Inventory.AddItem(_item);

        _model.gameObject.SetActive(false);
        if (_depletedOverride != null)
            _depletedOverride.gameObject.SetActive(true);

        if (_source != null)
            _source.Play();
    }

    private bool CanHarvest(Player player)
    {
        if (_requiredTool == RequiredToolType.Axe)
            return player.Inventory.HasTool(ItemTool.ToolType.Axe);
        if(_requiredTool == RequiredToolType.Pickaxe)
            return player.Inventory.HasTool(ItemTool.ToolType.Pickaxe);

        return true;
    }

    private void CreateAudioSource()
    {
        if (_harvestSound == null) return;
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = _harvestSound;
    }

    public enum RequiredToolType
    {
        None,
        Axe,
        Pickaxe
    }
}
