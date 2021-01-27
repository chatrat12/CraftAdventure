
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationEvents : MonoBehaviour
{
    public class StrikeWindowEvent : UnityEvent<Slot> { }

    public StrikeWindowEvent StrikeWindowOpened { get; } = new StrikeWindowEvent();
    public StrikeWindowEvent StrikeWindowClosed { get; } = new StrikeWindowEvent();

    private void AnimEvent_OpenWeaponStrikeWindow(Slot slot)
    {
        Debug.Log($"Open strike window {slot}");
        StrikeWindowOpened.Invoke(slot);
    }

    private void AnimEvent_CloseWeaponStrikeWindow(Slot slot)
    {
        Debug.Log($"Close strike window {slot}");
        StrikeWindowClosed.Invoke(slot);
    }

    public enum Slot
    {
        MainHand,
        OffHand
    }
}
