using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReFillPaint : MonoBehaviour
{
    [SerializeField] private InputActionReference _Click;
    [SerializeField] private GameObject[] _storagePopup = new GameObject[2];
    [SerializeField] private Image[] _paintCan = new Image[3];
    private bool _isFill;

    private void Update()
    {
        if (_isFill)
        {
            FillPaint();
        }
    }

    public void GetPaint()
    {
        if (!PlayerData.Instance._isStorage) return;
        StartCoroutine("getPaint");
    }

    private IEnumerator getPaint()
    {
        yield return new WaitForSeconds(1f);
        if (PlayerData.Instance._paintCanCount >= 3)
        {
            _storagePopup[0].SetActive(true);
            Debug.Log("페인트를 더 들고다닐 손이 없을텐데요. 부지런히 칠하세요!");
        }
        else
        {
            _storagePopup[1].SetActive(true); 
            
            _Click.action.Enable();
            _Click.action.performed += StartFill;
            _Click.action.canceled += StopFill;
        }
    }

    private void StartFill(InputAction.CallbackContext context)
    {
        Debug.Log("Fill 모드 시작");
        _isFill = true;
    }

    private void FillPaint()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_paintCan[i].fillAmount >= 1) return;
            _paintCan[i].fillAmount += Time.deltaTime;
        }
    }

    private void StopFill(InputAction.CallbackContext context)
    {
        _isFill = false;
    }

    /// ++ 페인트모드일때 위로 버튼 ㅇ
}
