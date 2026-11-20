using System;
using UnityEngine;
using UnityEngine.UI;

public class PaintCan: MonoBehaviour
{
    // 페인트 소비 -> fillAmount 줄어드는 부분 구현하기
    // 상점에서 페인트 리필하는 것 구현하기
    
    // 기본 1캔만 갖고있기.
    [SerializeField] private Image[] _paintCan = new Image[3];
    [SerializeField] private ModeController _modeController;

    private int _canCount = 3;
    
    public event Action ChangeCanEvent;

    private void OnEnable()
    {
        _modeController.MoveModeEvent += ChackCanCount;
        ChangeCanEvent += ChangeCan;
    }
    private void OnDisable()
    {
        _modeController.MoveModeEvent -= ChackCanCount;
        ChangeCanEvent -= ChangeCan;
    }
    private void ChackCanCount() // 이동모드때 검사, 상점에서 구비했을 때 검사.
    {
        int _count = 0;
        for (int i = 0; i < _paintCan.Length; i++)
        {
            if (_paintCan[i].fillAmount <= 0)
            {
            }
            else _count++;
        }
        if (_canCount != _count)
        {
            ChangeCanEvent?.Invoke();
        }
        _canCount = _count;
        Debug.Log("PaintCan : 소지 페인트 수" + _canCount);
    }
    private void ChangeCan() // 페인트통 소지수량이 스피드에 미치는 영향.
    {
        switch (_canCount)
        {
            case 0:
                PlayerData.Instance._playerSpeed += 0.5f;
                break;
            case 1:
                break;
            case 2:
                PlayerData.Instance._playerSpeed -= 0.5f;
                break;
            case 3:
                PlayerData.Instance._playerSpeed -= 1f;
                break;
        }
        Debug.Log("PaintCan : 페인트 소지 수량 변동, 플레이어 스피드가 달라집니다.");
    }
    
    
}
