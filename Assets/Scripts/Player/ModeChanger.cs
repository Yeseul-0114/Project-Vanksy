using UnityEngine;

public class ModeChanger : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _PaintButton;
    [SerializeField] private GameObject _MoveButton;
    
    private void Awake()
    {
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
    }

    public void PaintMode()
    {
        _PaintButton.SetActive(false);
        _MoveButton.SetActive(true);
    }

    public void MoveMode()
    {
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
    }
}
