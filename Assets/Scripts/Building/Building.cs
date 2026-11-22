using System;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] public ModeController _modeController;
    [SerializeField] public ModeButton _playermode;
    [SerializeField] public Image _minimap;
    [SerializeField] public bool _isClear; // 페인트완료 되면 여기 바꿔줘야함. 
    private Outline _outLine;
    private Painting _painting;
    private MeshRenderer _meshRenderer;
    private Building _building;
    public event Action OnClearEvent;
    
    private void Awake()
    {
        CachaComponent();
        
    }

    private void CachaComponent()
    {
        _building = GetComponent<Building>();
        _painting = GetComponent<Painting>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _outLine = GetComponent<Outline>();
        _outLine.enabled = false;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        // 클리어되지 않은 벽 앞에 있으면
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_isClear)
        {
            _outLine.enabled = true;
            _playermode._canPaint = true;
            _modeController.PaintModeEvent += OnPaint;
        }
    }

    private void OnPaint()
    {
        _painting.enabled = true;
        _minimap.color = Color.darkSalmon;
        if (!PlayerData.Instance._isStorage)
        {
            PlayerData.Instance._lastBuildingMR = _meshRenderer;
            PlayerData.Instance._lastBilding = _building;
        }
    }

    private void OnTriggerExit(Collider other) // 벽 칠하지 않고 지나쳤을 때
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_isClear)
        {
            _outLine.enabled = false;
            _playermode._canPaint = false;
            _modeController.PaintModeEvent -= OnPaint;
        }
    }

    // 점수 계산 로직 짜면 클리어 실행하기
    private void Clear()
    {
        OnClearEvent?.Invoke();
    }

}
