
using UnityEngine;

public class CharacterSlime : Character
{
    public MovementSlime Movement { get; private set; }

    public CharacterSlime(CharacterAvatar avatar, CharacterInfo info) : base(avatar, info)
    {
        Movement = new MovementSlime(this);
    }

    public override void Start()
    {
        var player = GameObject.FindObjectOfType<PlayerAvatar>();
        Movement.SetTarget(player.gameObject);
    }

    public override void OnAnimatorMove()
    {
        Movement.OnAnimatorMove();
    }

    public override void OnDrawGizmos()
    {
        Movement.OnDrawGizmos();
    }
}
