using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ModeButton : MonoBehaviour
{
    // 이 스크립트는 버튼 입력받았을 때 실행되는 것
    // setactive로 꺼질 위험이 있음.
    
    [SerializeField] private ModeController _modeController;
    public bool _canPaint = false;
    
    public void PaintMode()
    {
        if (!_canPaint && !PlayerData.Instance._isStorage)
        {
            Debug.Log("낙서 할 수 있는 벽이 없습니다.");
        }
        else if (PlayerData.Instance._isDiscover && !PlayerData.Instance._isStorage)
        {
            Debug.Log("범행을 들킨 상태입니다. 당분간 벽에 낙서 할 수 없습니다.");
        }
        else if (PlayerData.Instance._leftPaint <= 0 && !PlayerData.Instance._isStorage)
        {
            Debug.Log("페인트가 없습니다. 창고에서 가져오세요.");
        }
        else
        {
            _modeController.PaintMode();
        }
    }

    public void MoveMode()
    {
        _modeController.MoveMode();
    }
    
}
