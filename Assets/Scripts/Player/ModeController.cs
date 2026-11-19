using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ModeController : MonoBehaviour
{
    [Header("모드 버튼")]
    [SerializeField] private GameObject _PaintButton;
    [SerializeField] private GameObject _MoveButton;
    
    [Header("카메라 세팅")]
    [SerializeField] private GameObject _mainCamMove;
    [SerializeField] private Transform _paintVeiw;
    
    private Camera _cam;
    private Vector3 _mainCamPos;
    private Quaternion _mainCamRot;
    
    public event Action MoveModeEvent;
    public event Action PaintModeEvent;
    
    private void Awake()
    {
        CachaComponent();
    }

    public void PaintMode()
    {
        StartCoroutine(ToPaintMode());
        PlayerData.Instance._isPaintMode = true;
    }

    public void MoveMode()
    {
        StartCoroutine(ToMoveMode());
        PlayerData.Instance._isPaintMode = false;
    }

    public IEnumerator ToPaintMode()
    {
        _mainCamMove.GetComponent<CameraMove>().enabled = false;
        Debug.Log("ModeController : 페인트 모드로 들어갑니다.");
        
        // 페인트 내려놓는 애니메이션 실행
        yield return new WaitForSeconds(1f);
        PaintModeEvent?.Invoke(); // 페인트 모드 내용 실행
        _PaintButton.SetActive(false);
        _MoveButton.SetActive(true);
        
        // 카메라 시점 페인트모드로 변경
        _cam.transform.position = _paintVeiw.position;
        _cam.transform.rotation = _paintVeiw.rotation;
        _cam.fieldOfView += 40;
    }

    public IEnumerator ToMoveMode()
    {
        _mainCamMove.GetComponent<CameraMove>().enabled = true;
        Debug.Log("ModeController : 이동 모드로 들어갑니다.");
        
        // 페인트 들어올리는 애니메이션 실행
        yield return new WaitForSeconds(0.5f);
        MoveModeEvent?.Invoke(); // 이동 모드 내용 실행
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
        
        //카메라 시점 원복
        _cam.transform.position = _mainCamPos;
        _cam.transform.rotation = _mainCamRot;
        _cam.fieldOfView -= 40;
    }
    
    private void CachaComponent()
    {
        _cam = Camera.main;
        _mainCamPos = _cam.transform.position;
        _mainCamRot = _cam.transform.rotation;
        
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
    }
}
