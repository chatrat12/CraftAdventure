using UnityEngine;

public class HitTextManager : MonoBehaviour
{
    public const float HIT_TEXT_LIFE_TIME = 1f;

    public static ObjectPool<HitText> Pool
    {
        get
        {
            if(_instance == null)
            {
                var go = new GameObject("HitTexts");
                _instance = go.AddComponent<HitTextManager>();
            }
            return _instance._pool;
        }
    }

    private static HitTextManager _instance;

    private ObjectPool<HitText> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<HitText>(() =>
        {
            var result = Instantiate(Resources.Load<HitText>("UI/World/P_HitText"));
            result.transform.SetParent(transform);
            result.Init();
            return result;
        });
    }

    private void LateUpdate()
    {
        foreach (var hitText in _pool.CheckedOutObjects)
            hitText.UpdateHitText();
    }
}
