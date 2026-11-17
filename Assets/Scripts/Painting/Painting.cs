using UnityEngine;

public class Painting : MonoBehaviour
{
    [Range(0.01f, 1f)]
    [SerializeField] private float _brushSize = 0.03f;
    [SerializeField] private Texture2D _brushTexture;

    private Texture2D _mainTex;
    private MeshRenderer _mr;
    private RenderTexture _rt;

    private void Awake()
    {
        MrSetting();
    }

    private void Update()
    {
        
    }

    private void MrSetting()
    {
        TryGetComponent<MeshRenderer>(out _mr);
        _rt = new RenderTexture(512, 512, 32); // 텍스쳐의 가로 /세로 / 정밀도(비트 수)

        if (_mr.material.mainTexture != null)
        {
            _mainTex = _mr.material.mainTexture as Texture2D; // as 안전한 형변환 문법
        }
        else
        {
            _mainTex = new Texture2D(512, 512);
        }
        
        Graphics.Blit(_mainTex, _rt);
        _mr.material.mainTexture = _rt;

        if (_brushTexture == null)
        {
            Debug.Log("브러쉬 텍스처가 없습니다, 임시 텍스처를 적용합니다.");
            _brushTexture = new Texture2D(512, 512);
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    _brushTexture.SetPixel(i, j, Color.softYellow);
                }
            }
            _brushTexture.Apply();
        }
    }

    private void ToPainting()
    {
        
    }


}
