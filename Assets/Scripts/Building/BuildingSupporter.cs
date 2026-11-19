using UnityEngine;

public class BuildingSupporter : MonoBehaviour
{
    [SerializeField] private ModeController _controller;
    [SerializeField] private ModeButton _button;
    
    private Building[] _buildings;

    private void Awake() => CacheComponents();
    private void Start() => ReleasePlayerComponent();
    
    private void CacheComponents()
    {
        _buildings = GetComponentsInChildren<Building>();
    }

    private void ReleasePlayerComponent()
    {
        foreach (Building b in _buildings)
        {
            b._modeController = _controller;
            b._playermode = _button;
        }
    }
}
