public class Player
{
    public PlayerAvatar Avatar { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public UIPlayerMenus Menu { get; set; }
    public PlayerInteractDetection InteractDetection { get; private set; }
    public Inventory Inventory { get; private set; }
    public PlayerAnimation Animation { get; private set; }
    public CharacterAttachmentPoints Attachments { get; private set; }
    public EquipmentModels EquipmentModels { get; private set; }

    public Player(PlayerAvatar avatar)
    {
        Avatar = avatar;
        Inventory = new Inventory(32);
        Input = new PlayerInput(this);
        Movement = new PlayerMovement(this);
        InteractDetection = new PlayerInteractDetection(this);
        Animation = new PlayerAnimation(this);
        Attachments = avatar.GetComponent<CharacterAttachmentPoints>();
        EquipmentModels = avatar.GetComponent<EquipmentModels>();
    }

    public void Update()
    {
        Input.Update();
        Movement.Update();
        InteractDetection.Update();
    }

    public void OnAnimatorMove()
    {
        Movement.OnAnimatorMove();
    }
    public void OnDrawGizmos()
    {
        InteractDetection.OnDrawGizmos();
    }
}
