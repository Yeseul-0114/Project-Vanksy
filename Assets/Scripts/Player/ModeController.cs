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
    [SerializeField] private GameObject _neviMap;
    
    private Camera _cam;
    private Vector3 _mainCamPos;
    private Quaternion _mainCamRot;
    private float _keepPlayerSpeed;
    
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
        Debug.Log("ModeController : 이동 모드로 들어갑니다.");
        PlayerData.Instance._isPaintMode = false;
        
        MoveModeEvent?.Invoke(); 
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
        _neviMap.SetActive(false);
        PlayerData.Instance._playerSpeed = _keepPlayerSpeed;
        
        // 카메라 시점 원복
        _cam.fieldOfView -= 40;
        _cam.transform.position = _mainCamPos;
        _cam.transform.rotation = _mainCamRot;
        _mainCamMove.GetComponent<CameraMove>().enabled = true;
    }

    public IEnumerator ToPaintMode()
    {
        _mainCamMove.GetComponent<CameraMove>().enabled = false;
        Debug.Log("ModeController : 페인트 모드로 들어갑니다.");
        
        // 페인트 내려놓는 애니메이션 실행
        yield return new WaitForSeconds(1f);
        PaintModeEvent?.Invoke(); // 페인트 모드 내용 실행
        _neviMap.SetActive(true);
        _PaintButton.SetActive(false);
        _MoveButton.SetActive(true);
        PlayerData.Instance._playerSpeed = 0;
        
        // 카메라 시점 페인트모드로 변경
        _cam.transform.position = _paintVeiw.position;
        _cam.transform.rotation = _paintVeiw.rotation;
        _cam.fieldOfView += 40;
    }
    
    private void CachaComponent()
    {
        _keepPlayerSpeed = PlayerData.Instance._playerSpeed;
        
        _cam = Camera.main;
        _mainCamPos = _cam.transform.position;
        _mainCamRot = _cam.transform.rotation;
        
        _PaintButton.SetActive(true);
        _MoveButton.SetActive(false);
    }
}
