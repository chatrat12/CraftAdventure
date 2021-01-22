using UnityEngine;

public class CharacterAvatar : MonoBehaviour, IDamagable
{
    public Character Character { get; protected set; }
    public Damagable Damage => Character.Damage;
    [SerializeField] protected CharacterInfo _info;

    protected virtual void Awake()
    {
        Character = new Character(this, _info);
    }

    private void Start() => Character.Start();
    private void Update() => Character.Update();
    private void OnAnimatorMove() => Character.OnAnimatorMove();
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            Character.OnDrawGizmos();
    }
#endif
}
