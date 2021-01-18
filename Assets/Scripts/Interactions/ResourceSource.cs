﻿using UnityAsync;
using UnityEngine;

public class ResourceSource : InteractableObject
{
    [SerializeField] ItemStack _item;
    [SerializeField] Transform _model;
    [SerializeField] Transform _depletedOverride;
    [SerializeField] AudioClip _harvestSound;
    [SerializeField] string _interactMessage = "Interact";

    private AudioSource _source;

    public override string InteractMessage => _interactMessage;
    public bool Depleted { get; private set; }

    public override bool CanInteract(Player player) => !Depleted;

    private void Awake()
    {
        CreateAudioSource();
    }

    public override async void Interact(Player player)
    {
        player.Animation.PickupAnimation();
        await Await.Seconds(0.25f);

        player.Inventory.AddItem(_item);

        Depleted = true;
        _model.gameObject.SetActive(false);
        if (_depletedOverride != null)
            _depletedOverride.gameObject.SetActive(true);

        if (_source != null)
            _source.Play();
    }


    private void CreateAudioSource()
    {
        if (_harvestSound == null) return;
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = _harvestSound;
    }
}
