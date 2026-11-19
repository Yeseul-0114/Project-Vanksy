using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private ModeController _modeController;
    [SerializeField] private ModeButton _playermode;
    [SerializeField] public bool _isClear; // 페인트완료 되면 여기 바꿔줘야함. 
    private Outline _outLine;
    private Painting _painting;

    public event Action OnClearEvent;
    
    private void Awake()
    {
        CachaComponent();
        _painting = GetComponent<Painting>();
    }

    private void CachaComponent()
    {
        _outLine = GetComponent<Outline>();
        _isClear = false;
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
