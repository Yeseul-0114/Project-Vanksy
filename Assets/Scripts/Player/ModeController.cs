using System;
using System.Collections;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    [SerializeField] private GameObject _paintButton;
    [SerializeField] private GameObject _moveButton;
    [SerializeField] private PlayerData _playerMode;
    [SerializeField] private Transform _paintVeiw;
    
    private bool _canPaint = true;
    private Camera _cam;
    private Vector3 _mainCamPos;
    private Quaternion _mainCamRot;
    
    private void Awake()
    {
        _cam = Camera.main;
        _mainCamPos = _cam.transform.position;
        _mainCamRot = _cam.transform.rotation;
    }

    private void Start()
    {
        _paintButton.SetActive(true);
        _moveButton.SetActive(false);
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
            _playerMode._isPaintMode = true;
            Debug.Log("ModeController : 페인트 모드로 들어갑니다.");
            StartCoroutine(paintMode());
        }
    }

    private IEnumerator paintMode()
    {
        // 페인트 내려놓는 애니메이션 실행
        yield return new WaitForSeconds(1f);
        _paintButton.SetActive(false);
        _moveButton.SetActive(true);
        // 페인트 모드 
        _cam.transform.position = _paintVeiw.position;
        _cam.transform.rotation = _paintVeiw.rotation;
    }
    //---------------------------------------------------

    public void MoveMode()
    {
        _playerMode._isPaintMode = false;
        Debug.Log("ModeController : 이동 모드로 들어갑니다.");
        StartCoroutine(moveMode());
    }

    private IEnumerator moveMode()
    {
        // 페인트 들어올리는 애니메이션 실행
        yield return new WaitForSeconds(0.5f);
        _paintButton.SetActive(true);
        _moveButton.SetActive(false);
        // 이동모드로 전환_ 페인트 모드 내리기 
        _cam.transform.position = _mainCamPos;
        _cam.transform.rotation = _mainCamRot;
    }
    //---------------------------------------------------


}
