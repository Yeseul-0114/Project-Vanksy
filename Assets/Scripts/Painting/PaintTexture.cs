using UnityEngine;

/*
 * - 그림 그릴 대상 게임오브젝트에 컴포넌트로 넣기
 * 
 */
public class PaintTexture : MonoBehaviour
{
    public int resolution = 512; // 이거 사이즈네
    [Range(0.01f, 1f)] // 이게 브러쉬 사이즈 조절가능 범위인가봄
    public float brushSize = 0.1f;
    public Texture2D brushTexture; // 브러시 텍스쳐인가봄

    private Texture2D mainTex; // 메인텍스쳐는 기본 텍스쳐인가봄
    private MeshRenderer mr; //이건 렌더러 형태 그 자체
    private RenderTexture rt; // 이건 렌더러 텍스쳐 눈에 보이는 거

    private void Awake()
    {
        TryGetComponent<MeshRenderer>(out mr); // 컴포넌트 중 있으면 mr에 넣어라
        rt = new RenderTexture(resolution, resolution, 32); 

        if (mr.material.mainTexture != null) // 렌더러의 메인 텍스쳐가 null이 아니면 실행해
        {
            mainTex = mr.material.mainTexture as Texture2D; // 메쉬렌더러의 텍스처를 메인텍스처에 넣어라?
        }
        // 메인 텍스쳐가 없을 경우, 하얀 텍스쳐를 생성하여 사용
        else
        {
            mainTex = new Texture2D(resolution, resolution); // 널이면 이거 넣어라, 이거 사이즈 512 * 512 짜리
        }

        // 메인 텍스쳐 -> 렌더 텍스쳐 복제
        Graphics.Blit(mainTex, rt);

        // 렌더 텍스쳐를 메인 텍스쳐에 등록
        mr.material.mainTexture = rt;

        // 브러시 텍스쳐가 없을 경우 임시 생성(red 색상)
        if (brushTexture == null)
        {
            brushTexture = new Texture2D(resolution, resolution);
            for (int i = 0; i < resolution; i++)
                for (int j = 0; j < resolution; j++)
                    brushTexture.SetPixel(i, j, Color.red);
            brushTexture.Apply();
        }
    }

    //================================================================================================
    
    private void Update() // 이건 업데이트에서 클릭 검사하는 것.
    {
        // NOTE : 텍스쳐 페인팅의 대상이 될 모든 컴포넌트에서 레이캐스트 검사를 수행하므로 비효율적이다.
        // 실제로 사용하려면 하나의 컴포넌트에서 레이캐스트 수행하도록 구조를 변경해야 한다.

        // 마우스 클릭 지점에 브러시로 그리기
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool raycast = Physics.Raycast(ray, out var hit);
            Collider col = hit.collider;

            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);

            // 본인이 레이캐스트에 맞았으면 그리기
            if (raycast && col && col.transform == transform)
            {
                Vector2 pixelUV = hit.textureCoord;
                pixelUV *= resolution;
                DrawTexture(pixelUV);
            }
        }
    }

    /// <summary> 렌더 텍스쳐에 브러시 텍스쳐로 그리기 </summary>
    public void DrawTexture(in Vector2 uv)
    {
        RenderTexture.active = rt; // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당
        GL.PushMatrix();                                  // 매트릭스 백업
        GL.LoadPixelMatrix(0, resolution, resolution, 0); // 알맞은 크기로 픽셀 매트릭스 설정

        float brushPixelSize = brushSize * resolution;

        // 렌더 텍스쳐에 브러시 텍스쳐를 이용해 그리기
        Graphics.DrawTexture(
            new Rect(
                uv.x - brushPixelSize * 0.5f,
                (rt.height - uv.y) - brushPixelSize * 0.5f,
                brushPixelSize,
                brushPixelSize
            ),
            brushTexture
        );

        GL.PopMatrix();              // 매트릭스 복구
        RenderTexture.active = null; // 활성 렌더 텍스쳐 해제
    }
}