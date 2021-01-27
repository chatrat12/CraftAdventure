using UnityEngine;

public class EquipmentModels : MonoBehaviour
{
    public Transform WoodAxe { get; private set; }
    public Transform Sword { get; private set; }

    [SerializeField] GameObject _woodAxe;
    [SerializeField] GameObject _sword;

    private void Awake()
    {
        WoodAxe = InitModel(_woodAxe);
        Sword = InitModel(_sword);
    }

    private Transform InitModel(GameObject prefab)
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        return go.transform;
    }
}
