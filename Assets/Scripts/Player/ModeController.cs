using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ModeController : MonoBehaviour
{
    [Header("모드 버튼")]
    [SerializeField] private GameObject _PaintButton;
    [SerializeField] private GameObject _MoveButton;
    [SerializeField] private ReFillPaint _fillPaint;
    
    [Header("카메라 세팅")]
    [SerializeField] private GameObject _mainCamMove;
    [SerializeField] private Transform _paintVeiw;
    [SerializeField] private GameObject _neviMap;
    
    private Camera _cam;
    private Vector3 _mainCamPos;
    private Quaternion _mainCamRot;
    
    public event Action MoveModeEvent;
    public event Action PaintModeEvent;
    
    private void Awake()
    {
        CacheComponent();
    }

    public void PaintMode()
    {
        StartCoroutine(ToPaintMode());
        PlayerData.Instance._isPaintMode = true;
    }

    public void MoveMode()
    {
        PlayerData.Instance._isPaintMode = false;
        
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
        _neviMap.SetActive(true);
        MoveModeEvent?.Invoke(); 
        
        // 카메라 시점 원복
        _cam.fieldOfView = 32;
        _cam.transform.position = _mainCamPos;
        _cam.transform.rotation = _mainCamRot;
        _mainCamMove.GetComponent<CameraMove>().enabled = true;
    }

    private IEnumerator ToPaintMode()
    {
        _mainCamMove.GetComponent<CameraMove>().enabled = false;
        
        // 페인트 내려놓는 애니메이션 실행
        yield return new WaitForSeconds(1f);

        if (PlayerData.Instance._isStorage)
        {
            _fillPaint.UseingStorage();
        }

        PaintModeEvent?.Invoke(); // 페인트 모드 내용 실행
        _neviMap.SetActive(false);
        _PaintButton.SetActive(false);
        _MoveButton.SetActive(true);
        if (!PlayerData.Instance._isStorage)
        {
            PlayerData.Instance._playerSpeed = 0;
        }
        
        // 카메라 시점 페인트모드로 변경
        _cam.transform.position = _paintVeiw.position;
        _cam.transform.rotation = _paintVeiw.rotation;
        _cam.fieldOfView += 40;
    }
    
    private void CacheComponent()
    {
        _cam = Camera.main;
        _mainCamPos = _cam.transform.position;
        _mainCamRot = _cam.transform.rotation;
        
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
    }
}
