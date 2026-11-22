using System;
using UnityEngine;
using UnityEngine.UI;

public class PaintCan: MonoBehaviour
{
    // 페인트 소비 -> fillAmount 줄어드는 부분 구현하기
    
    // 기본 1캔만 갖고있기.
    [SerializeField] private Image[] _paintCan = new Image[3];
    [SerializeField] private ModeController _modeController;
    
    private void OnEnable()
    {
        _modeController.MoveModeEvent += ChackCanCount;
    }
    private void OnDisable()
    {
        _modeController.MoveModeEvent -= ChackCanCount;
    }
    public void ChackCanCount() // 이동모드때 검사, 상점에서 구비했을 때 검사.
    {
        if (PlayerData.Instance._arrest) return;
        
        int _count = 0;
        for (int i = 0; i < _paintCan.Length; i++)
        {
            if (_paintCan[i].fillAmount > 0) _count++;
        }
        
        // 소지 수량 대비 플레이어 속도 재세팅
        switch (_count)
        {
            case 0:
                PlayerData.Instance._playerSpeed = 3.5f;
                break;
            case 1:
                PlayerData.Instance._playerSpeed = 2f;
                break;
            case 2:
                PlayerData.Instance._playerSpeed = 1.5f;
                break;
            case 3:
                PlayerData.Instance._playerSpeed = 1f;
                break;
        }

        if (PlayerData.Instance._paintCanCount != _count)
        {
            Debug.Log($"PaintCan : 소지 페인트 수 달라졌습니다, 플레이어 속도 바뀝니다.");
        }
        PlayerData.Instance._paintCanCount = _count;
    }
}
