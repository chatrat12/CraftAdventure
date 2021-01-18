using UnityAsync;
using UnityEngine;

public class PlayerAnimation
{
    private Animator _animator;
    private Player _player;
    public PlayerAnimation(Player player)
    {
        _player = player;
        _animator = player.Avatar.GetComponent<Animator>();
    }

    public void PickupAnimation()
    {
        _animator.SetTrigger("Pickup");
    }

    public async void ChopWood()
    {
        _player.EquipmentModels.WoodAxe.gameObject.SetActive(true);
        _player.Attachments.RightHand.Equip(_player.EquipmentModels.WoodAxe);
        _animator.SetTrigger("AxeSwing");
        await Await.Seconds(0.5f);
        _player.Attachments.RightHand.Clear();
    }
}
