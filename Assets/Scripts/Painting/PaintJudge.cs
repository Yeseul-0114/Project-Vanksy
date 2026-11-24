using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 두 컴포넌트가 있어야 가능.
[RequireComponent(typeof(Building), typeof(Painting))]
public class PaintJudge : MonoBehaviour
{
    [SerializeField] private Image[] _paintTin;
    [SerializeField] private float _clearRatio = 0.2f;
    [SerializeField] private TextMeshProUGUI _paintRatioUI;

    private Building _building;
    private Painting _painting;
    private SphereCollider _trigger;
    private RenderTexture _paint;
    private Color32[] _before;
    private bool _isPlayerNear;
    private bool _isPainting;
    private bool _isCleared;
    
    public float Ratio { get; private set; }

#region UnityEventMethods

private void Awake()
{
    CacheComponents();
}

    private void OnEnable() => SubscribePaintEvents();
    private void OnTriggerEnter(Collider other) => SetPlayerNear(other, true);
    private void OnTriggerExit(Collider other) => SetPlayerNear(other, false);

    // Start 반환형을 IEnumerator로 처리하면 Unity 자체적으로 Start를 코루틴으로 처리
    private IEnumerator Start()
    {
        yield return null; // 1 프레임을 기다리게 할 용도
        TakeInitPhoto();
    }

    private void Update() => TryPeriodicJudge();
    private void OnDisable() => UnsubscribePaintEvents();
#endregion

#region Initialization
    private void CacheComponents()
    {
        _building = GetComponent<Building>();
        _painting = GetComponent<Painting>();
        _trigger = GetComponent<SphereCollider>();
        if (!TryGetComponent(out MeshCollider _)) gameObject.AddComponent<MeshCollider>();
    }

    private void TakeInitPhoto()
    {
        _paint = GetComponent<MeshRenderer>().material.mainTexture as RenderTexture;
        _before = TakePhoto(); // 이전꺼 저장
    }
#endregion

#region PaintEvents
    private void SubscribePaintEvents()
    {
        _painting._modeController.PaintModeEvent += OnPaintStart;
        _painting._modeController.PaintModeEvent += PopupRatioInfo;
        _painting._modeController.MoveModeEvent += OnPaintEnd;
        _painting._modeController.MoveModeEvent += CloseRatioUI;
    }

    private void UnsubscribePaintEvents()
    {
        _painting._modeController.PaintModeEvent -= OnPaintStart;
        _painting._modeController.PaintModeEvent -= PopupRatioInfo;
        _painting._modeController.MoveModeEvent -= OnPaintEnd;
        _painting._modeController.MoveModeEvent -= CloseRatioUI;
    }

    // 이벤트는 모든 건물이 받으므로, 플레이어가 앞에 있는 건물만 시작
    private void OnPaintStart() => _isPainting = _isPlayerNear;

    private void OnPaintEnd()
    {
        if (!_isPainting) return;

        _isPainting = false;
        TryJudge();
    }

    private void SetPlayerNear(Collider other, bool isNear) // 트리거 감지로 작동
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) _isPlayerNear = isNear;
    }
#endregion

#region Judge
    private void TryPeriodicJudge()
    {
        // 페인팅 중이면서 30 프레임마다
        if (_isPainting && Time.frameCount % 60 == 0) TryJudge();
    }

    private void TryJudge() // 트라이
    {
        if (_before == null || _isCleared) return;

        Judge();
    }

    private void Judge() // 실제 기능_ 판정
    {
        Color32[] currnet = TakePhoto(); 
        int count = 0;

        for (int i = 0; i < currnet.Length; i++)
        {
            if (!currnet[i].Equals(_before[i])) count++;
        }

        Ratio = (float)count / currnet.Length;
        
        // 건물 1개 클리어 : Ratio = 0.2 // 1통당 건물 1개 
        SpendPaint();
        PaintRatioUI();
        
        Debug.Log($"채색 퍼센트 {Ratio * 400}");
        
        if (Ratio >= _clearRatio) StartCoroutine(ClearBuilding());
    }
#endregion

private void SpendPaint()
{
    if (_paintTin[0].fillAmount <= 0)
    {
        Debug.Log("페인트 없음.");
        // 페인트 중지 -> "페인트가 없습니다" 메시지 띄우기.
    }
    else
    {
       // 페인트 용량 줄어드는 것 구현
    }
}

private void PopupRatioInfo() => _paintRatioUI.gameObject.SetActive(true);

private void CloseRatioUI()
{
    _paintRatioUI.gameObject.SetActive(false);

    if ((Ratio*400 >= 100))
    {
        _building._minimap.color = Color.springGreen;
    }
    else if ((Ratio * 400) >= 80)
    {
        _building._minimap.color = Color.darkGreen;
    }
    else if ((Ratio * 400) >= 40)
    {
        _building._minimap.color = Color.darkOrange;
    }
    else if ((Ratio * 400) >= 1)
    {
        _building._minimap.color = Color.softYellow;
    }
}

private void PaintRatioUI()
{
    if (Ratio * 400 >= 100)
    {
        _paintRatioUI.text = "Perfect!";
        _paintRatioUI.color = Color.purple;
       
    }
    else if (Ratio * 400 >= 80)
    {
        _paintRatioUI.text = "Good.";
        _paintRatioUI.color = Color.dodgerBlue;
    }
    else if (Ratio * 400 >= 50)
    {
        _paintRatioUI.text = "Close!";
        _paintRatioUI.color = Color.darkOrange;
    }
    else if (Ratio * 400 >= 40)
    {
        _paintRatioUI.text = "hum.... More.";
        _paintRatioUI.color = Color.yellow;
    }
    else
    {
        _paintRatioUI.text = "Painting!!";
        _paintRatioUI.color = Color.azure;
    }
}

#region Clear
    private IEnumerator ClearBuilding()
    {
        _isCleared = true;
        // 종료 이벤트가 물리 콜백에서 와도 아래 순서가 유지되도록, 물리 단계를 벗어날 때까지 대기
        yield return null;

        // 감지 영역을 멀리 치워서 Building.OnTriggerExit을 호출
        _trigger.center += Vector3.down * 1000f;
        // Building.OnTriggerExit을 먼저 처리해야 할 것이므로 다음 FixedUpdate까지 대기
        yield return new WaitForFixedUpdate();

        _building._isClear = true;
        PlayerData.Instance._clearBildings++;
        
        // OnPaintClear?.Invoke();
    }
#endregion

#region PhotoUtil
    private Color32[] TakePhoto()
    {
        Texture2D photo = new Texture2D(_paint.width, _paint.height);
        RenderTexture.active = _paint;
        photo.ReadPixels(new Rect(0, 0, _paint.width, _paint.height), 0, 0);
        RenderTexture.active = null;

        Color32[] pixels = photo.GetPixels32();
        Destroy(photo);
        return pixels;
    }
#endregion
}