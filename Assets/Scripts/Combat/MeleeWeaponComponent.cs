using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponComponent : EquipmentComponent
{
    private List<IDamagable> _objectsStruckThisWindow = new List<IDamagable>();
    private OverlapDetector _overlapDetector;
    private bool _strickWindowOpen = false;

    private void Awake()
    {
        _overlapDetector = GetComponent<OverlapDetector>();
        _overlapDetector.Mode = OverlapDetector.OperatingMode.Explicit;
    }

    public void OpenStrikeWindow()
    {
        _strickWindowOpen = true;
        _objectsStruckThisWindow.Clear();
    }
    public void CloseStrikeWindow()
    {
        _strickWindowOpen = false;
    }

    private void Update()
    {
        if (!_strickWindowOpen) return;
        foreach (var overlap in _overlapDetector.Overlaps)
        {
            var damagable = overlap.GetComponentInParent<IDamagable>();
            if (damagable != null && !_objectsStruckThisWindow.Contains(damagable))
            {
                Debug.Log($"Struck {overlap.gameObject.name}");
                _objectsStruckThisWindow.Add(damagable);
            }
        }
    }
}