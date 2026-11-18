using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Transform _paintVeiw;
    [SerializeField] private ModeChanger _modeChanger;
    [SerializeField] private GameObject _CamMove;
    
    public bool _canPaint = false;
    private Camera _cam;
    private Vector3 _mainCamPos;
    private Quaternion _mainCamRot;
    private void Awake()
    {
        _cam = Camera.main;
        _mainCamPos = _cam.transform.position;
        _mainCamRot = _cam.transform.rotation;
    }

    //---------------------------------------------------
    public void PaintMode()
    {
        if (!_canPaint)
        {
            Debug.Log("낙서 할 수 있는 벽이 없습니다.");
        }
        else
        {
            _playerData._isPaintMode = true;
            Debug.Log("ModeController : 페인트 모드로 들어갑니다.");
            StartCoroutine(ToPaintMode());
        }
    }

    private IEnumerator ToPaintMode()
    {
        // 페인트 내려놓는 애니메이션 실행
        yield return new WaitForSeconds(1f);
        _CamMove.GetComponent<CameraMove>().enabled = false;
        _modeChanger.PaintMode();
        // 페인트 모드 
        _cam.transform.position = _paintVeiw.position;
        _cam.transform.rotation = _paintVeiw.rotation;
    }
    //---------------------------------------------------

    public void MoveMode()
    {
        _playerData._isPaintMode = false;
        Debug.Log("ModeController : 이동 모드로 들어갑니다.");
        StartCoroutine(ToMoveMode());
    }

    private IEnumerator ToMoveMode()
    {
        // 페인트 들어올리는 애니메이션 실행
        yield return new WaitForSeconds(0.5f);
        _CamMove.GetComponent<CameraMove>().enabled = true;
        _modeChanger.MoveMode();
        // 이동모드로 전환_ 페인트 모드 내리기 
        _cam.transform.position = _mainCamPos;
        _cam.transform.rotation = _mainCamRot;
    }
    //---------------------------------------------------
}
