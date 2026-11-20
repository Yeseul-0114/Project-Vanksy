using UnityEngine;

public class BuildingSupporter : MonoBehaviour
{
    [SerializeField] private ModeController _controller;
    [SerializeField] private ModeButton _button;
    
    private Building[] _buildings;
    // private Painting[] _paintings;

    private void Awake() => CacheComponents();
    private void Start() => ReleasePlayerComponent();
    
    private void CacheComponents()
    {
        _buildings = GetComponentsInChildren<Building>();
       // _paintings = GetComponentsInChildren<Painting>();
    }

    private void ReleasePlayerComponent()
    {
        foreach (Building b in _buildings)
        {
            b._modeController = _controller;
            b._playermode = _button;
        }

        /*foreach (Painting p in _paintings)
        {
            p._modeController = _controller;
        }*/
    }
}
