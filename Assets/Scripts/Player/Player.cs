﻿public class Player : Character
{
    public PlayerInput               Input              { get; private set; }
    public PlayerMovement            Movement           { get; private set; }
    public PlayerInteractDetection   InteractDetection  { get; private set; }
    public Inventory                 Inventory          { get; private set; }
    public PlayerAnimation           Animation          { get; private set; }
    public CharacterAttachmentPoints Attachments        { get; private set; }
    public EquipmentModels           EquipmentModels    { get; private set; }
    public UIPlayerMenus             Menu               { get; set; }
    public ThirdPersonCamera         CameraRig          { get; set; }

    public Player(PlayerAvatar avatar, CharacterInfo info) : base(avatar, info)
    {
        Inventory = new Inventory(32);
        Input = new PlayerInput(this);
        Movement = new PlayerMovement(this);
        InteractDetection = new PlayerInteractDetection(this);
        Animation = new PlayerAnimation(this);
        Attachments = avatar.GetComponent<CharacterAttachmentPoints>();
        EquipmentModels = avatar.GetComponent<EquipmentModels>();
    }

    public override void Update()
    {
        Input.Update();
        Movement.Update();
        InteractDetection.Update();
    }

    public override void OnAnimatorMove()
    {
        Movement.OnAnimatorMove();
    }
    public override void OnDrawGizmos()
    {
        InteractDetection.OnDrawGizmos();
    }
}
