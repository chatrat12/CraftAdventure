using UnityEngine;

public class EquipmentModels : MonoBehaviour
{
    public Transform WoodAxe { get; private set; }

    [SerializeField] GameObject _woodAxe;

    private void Awake()
    {
        WoodAxe = InitModel(_woodAxe);
    }

    private Transform InitModel(GameObject prefab)
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        return go.transform;
    }
}
