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
}
