using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReFillPaint : MonoBehaviour
{
    [Header("기본 세팅")]
    [SerializeField] private ModeController _modeController;
    [SerializeField] private InputActionReference _Click;
    
    
    [Header("페인트 저장소용")]
    [SerializeField] private GameObject[] _storagePopup = new GameObject[2];
    [SerializeField] private Image[] _paintCan = new Image[3];
    private bool _isFill;
    
    
    private void Update()
    {
        if (_isFill) FillPaint();
    }

    private void OnEnable() => _modeController.MoveModeEvent += closePopup;
    private void OnDisable() => _modeController.MoveModeEvent -= closePopup;

    public void UseingStorage()
    {
        if (!PlayerData.Instance._canFill)
        {
            _storagePopup[0].SetActive(true);
            Debug.Log("페인트를 더 들고다닐 손이 없을텐데요. 부지런히 칠하세요!");
        }
        else
        {
            _storagePopup[1].SetActive(true);
            
            PlayerData.Instance._UseingStorage++;
            _Click.action.Enable();
            _Click.action.performed += StartFill;
            _Click.action.canceled += StopFill;
        }
    }

    private void closePopup()
    {
        _storagePopup[0].SetActive(false);
        _storagePopup[1].SetActive(false);
    }

    //==============================================================
    private void StartFill(InputAction.CallbackContext context)
    {
        Debug.Log("Fill 모드 시작");
        _isFill = true;
    }

    private void FillPaint() // 누르는 동안 페인트 리필 -> 속도 조절 + 동시에 차지않고 순서대로 차도록 수정하기
    {
        if (_paintCan[0].fillAmount < 1)
        {
            _paintCan[0].fillAmount += Time.deltaTime/3;
        }
        else
        {
            for (int i = 1; i < 3; i++)
            {
                if (_paintCan[i].fillAmount < 1 && _paintCan[i-1].fillAmount >=1)
                {
                    _paintCan[i].fillAmount += Time.deltaTime/3;
                }
            }
        }
    }

    private void StopFill(InputAction.CallbackContext context)
    {
        _isFill = false;
    }
}
