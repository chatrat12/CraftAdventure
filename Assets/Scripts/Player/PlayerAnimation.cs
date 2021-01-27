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

    public async void ChopWood(ItemEquipment axe)
    {
        _player.Equipment.AttachmentPoints.PrimaryWeapon.Sheathed = true;
        var model = _player.Equipment.ModelCache.GetModel(axe);
        _player.Equipment.AttachmentPoints.MainHandUtility.Attach(model.transform);
        _animator.SetTrigger("AxeSwing");
        await Await.Seconds(0.5f);
        _player.Equipment.AttachmentPoints.MainHandUtility.Clear();
    }

    public void SwingSword()
    {
        _animator.SetTrigger("SwordSwing");
    }
}
