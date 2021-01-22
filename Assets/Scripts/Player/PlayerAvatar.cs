using UnityEngine;

public class PlayerAvatar : CharacterAvatar
{
    //public Player Player { get; private set; }

    protected override void Awake()
    {
        Character = new Player(this, _info);
    }

}