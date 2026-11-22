using System;
using System.Collections;
using UnityEngine;

// 두 컴포넌트가 있어야 가능.
[RequireComponent(typeof(Building), typeof(Painting))]
public class PaintJudge : MonoBehaviour
{
    [SerializeField] private float _clearRatio = 0.5f;

    private Building _building;
    private Painting _painting;
    private SphereCollider _trigger;
    private RenderTexture _paint;
    private Color32[] _before;
    private bool _isPlayerNear;
    private bool _isPainting;
    private bool _isCleared;

    public float Ratio { get; private set; }
    public event Action OnPaintClear;

#region UnityEventMethods
    private void Awake() => CacheComponents();
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
        _painting._modeController.MoveModeEvent += OnPaintEnd;
    }

    private void UnsubscribePaintEvents()
    {
        _painting._modeController.PaintModeEvent -= OnPaintStart;
        _painting._modeController.MoveModeEvent -= OnPaintEnd;
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
        if (_isPainting && Time.frameCount % 30 == 0) TryJudge();
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
        Debug.Log($"{name} : {Ratio:P1}");

        if (Ratio >= _clearRatio) StartCoroutine(ClearBuilding());
    }
#endregion

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
        _building._minimap.color = Color.darkGreen;
        OnPaintClear?.Invoke();
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