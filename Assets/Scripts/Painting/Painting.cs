using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Painting : MonoBehaviour
{
    [Header("기본 세팅")]
    [SerializeField]
    public ModeController _modeController;
    [SerializeField] private InputActionReference _click;
    [SerializeField] private InputActionReference _clickPos;

    [Header("브러쉬 세팅")] 
    [SerializeField] private int _resolution = 512; // 가로세로 픽셀 수, 해상도. 512 x 512
    [SerializeField] private float _brushSize = 0.03f;
    [SerializeField] private Texture2D _brushTexture;
    
    private Texture2D _mainTex;
    private MeshRenderer _mr;
    private RenderTexture _rt;
    private Camera _cam;
    private bool _isClick;

    private void Awake()
    {
        MrSetting();
        CaChaComponent();
        this.enabled = false;
    }

    private void Update()
    {
        ReadClick();
    }
    
    private void CaChaComponent()
    {
        _modeController.PaintModeEvent += InPainting;
        _modeController.MoveModeEvent += OutPainting;
        _cam = Camera.main;
    }
    
    private void OnClick(InputAction.CallbackContext context) => _isClick = true;
    private void DisClick(InputAction.CallbackContext context) => _isClick = false;
    
    private void InPainting()
    {
        _click.action.Enable();
        _clickPos.action.Enable();
        _click.action.performed += OnClick;
        _click.action.canceled += DisClick;
    }

    private void OutPainting()
    {
        _clickPos.action.Disable();
        _click.action.Disable();
        this.enabled = false;
    }

    private void ReadClick()
    {
        if (_isClick)
        {
            Vector2 _brushPos;
            _brushPos = _clickPos.action.ReadValue<Vector2>();
        
            Ray ray = _cam.ScreenPointToRay(_brushPos);
            RaycastHit hit;
        
            // Ray에 맞은게 본인이라면 실행
            if (Physics.Raycast(ray, out hit) && hit.collider.transform == transform)
            {
                Vector2 _pixelUV = hit.textureCoord; // textureCoord : 레이에 맞는 콜라이더의 UV좌표
                _pixelUV *= _resolution; 
                // 좌표값에 텍스쳐 사이즈만큼 곱한다_ 좌표값은 0~1로 되어있음. 실제 픽셀 좌표를 알기위해서는 텍스처의 사이즈를 곱할 필요가 있다.
                // 브러시를 그릴 위치와 크기를 텍스처의 픽셀 단위로 계산하기 위해 UV 좌표를 변환
                ToPainting(_pixelUV);
            }
        }
    }

    private void ToPainting(in Vector2 pixelUV) // in : 읽기전용 매개변수
    {
        RenderTexture.active = _rt; // 페인팅위해서 활성 텍스처 임시 할당 
        GL.PushMatrix();  // 현재 카메라·렌더링 행렬을 스택에 보관
        GL.LoadPixelMatrix(0, _resolution, 0, _resolution);
        
        float brushPixelSize = _brushSize * _resolution; // 설정한 사이즈 x 픽셀수 = 해당 브러쉬가 차지하는 픽셀 수
        
        Graphics.DrawTexture(new Rect(pixelUV.x - brushPixelSize * 0.5f ,pixelUV.y - brushPixelSize * 0.5f,brushPixelSize,brushPixelSize), _brushTexture);
        
        GL.PopMatrix(); // GL.PushMatrix(); 이때 보관한 것 복구_ 둘이 세트.
        RenderTexture.active = null; // 활성 렌더 텍스쳐 해제
    }
 
    private void MrSetting()
    {
        TryGetComponent<MeshRenderer>(out _mr);
        _rt = new RenderTexture(_resolution, _resolution, 32); // 텍스쳐의 가로 /세로 / 정밀도(비트 수)

        if (_mr.material.mainTexture != null)
        {
            _mainTex = _mr.material.mainTexture as Texture2D; // as 안전한 형변환 문법
        }
        else
        {
            _mainTex = new Texture2D(_resolution, _resolution);
        }
        
        // 메인 텍스쳐 -> 렌더 텍스쳐 복제
        Graphics.Blit(_mainTex, _rt);
        
        // 렌더 텍스쳐를 메인 텍스쳐에 등록
        _mr.material.mainTexture = _rt;

        // 브러시 텍스쳐가 없을 경우 임시 생성
        if (_brushTexture == null)
        {
            // Debug.Log("브러쉬 텍스처가 없습니다, 임시 텍스처를 적용합니다.");
            _brushTexture = new Texture2D(_resolution, _resolution);
            for (int i = 0; i < _resolution; i++)
            {
                for (int j = 0; j < _resolution; j++)
                {
                    _brushTexture.SetPixel(i, j, Color.dodgerBlue);
                }
            }
            _brushTexture.Apply();
        }
    }
}
