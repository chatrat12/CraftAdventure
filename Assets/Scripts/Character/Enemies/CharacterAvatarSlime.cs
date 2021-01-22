public class CharacterAvatarSlime : CharacterAvatar
{
    protected override void Awake()
    {
        Character = new CharacterSlime(this, _info);
    }
}
