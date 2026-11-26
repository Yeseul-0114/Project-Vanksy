using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSupporter : MonoBehaviour
{
    [SerializeField] private ModeController _controller;
    [SerializeField] private ModeButton _button;
    [SerializeField] private Image[] _paintTin; 
    [SerializeField] private TextMeshProUGUI _paintRatioUI;
    
    private Building[] _buildings; 
    private Painting[] _paintings;
    private PaintJudge[] _judges;

    private void Awake() => CacheComponents();
    private void Start() => ReleasePlayerComponent();
    
    private void CacheComponents()
    {
        _buildings = GetComponentsInChildren<Building>();
        _paintings = GetComponentsInChildren<Painting>();
        _judges = GetComponentsInChildren<PaintJudge>();
    }

    private void ReleasePlayerComponent()
    {
        foreach (Building b in _buildings)
        {
            b._modeController = _controller;
            b._playermode = _button;
        }

        foreach (Painting p in _paintings)
        {
            p._modeController = _controller;
        }
        
        foreach (PaintJudge j in _judges)
        {
            j._paintTin = _paintTin;
            j._paintRatioUI = _paintRatioUI;
            j._modeController = _controller;
        }
    }
}
