using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    public Player Player { get; private set; }

    private void Awake()
    {
        Player = new Player(this);
    }

    private void Update() => Player.Update();
    private void OnAnimatorMove() => Player.OnAnimatorMove();
    private void OnDrawGizmos() => Player.OnDrawGizmos();
}