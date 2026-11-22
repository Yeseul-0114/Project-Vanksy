using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NeighborController : MonoBehaviour, IStateEvent
{
    [SerializeField] private PaintTin _paintCan;
    [SerializeField] private PosTarget _posTarget;
    [SerializeField] private Image[] _paintCanImages;
    [SerializeField] private Image _miniMap;
    [SerializeField] private Material _resetMR;
    [SerializeField] private float _catchRange = 0.7f; // 잠기 판정범위
    
   // public bool _arrest; // 범인 잡았니
    private GameObject _Taget; // 범인
    internal bool _playerMarking; // 타겟 감지했음
    // internal Vector3 _lastBildingPos; // 범행현장
    private float _detectRange = 3f; // 추격 판정 범위
    
    internal NavMeshAgent _neighborAgent;
    internal Vector3 _targetDirection; // 목적지 좌표. 
    internal Animator _animator;
    //====================================================
   
    private StateBase _idle;
    private StateBase _walking;
    private StateBase _tracking;
    private StateBase _anger;
    
    public Action<StateBase> OnStateChange { get; set; }
    
    public StateBase IdleState => _idle;
    public StateBase Walking => _walking;
    public StateBase Tracking => _tracking;
    public StateBase Anger => _anger;

    private void Awake()
    {
        CacheComponent();
        
        _idle = new IdleState(this);
        _walking = new WalkingState(this);
        _tracking = new TrackingState(this);
        _anger = new AngerState(this);
    }
    private void CacheComponent()
    {
        _neighborAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    

    private void Start()
    {
        OnStateChange?.Invoke(_walking);
    }
    
    public void SetState(StateBase state)
    {
        OnStateChange?.Invoke(state);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRange); // 놓쳤음 판정 범위
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _catchRange ); // 잡았음 판정 범위
    }

    //========================================================
    
    //부채꼴 
    // 트리거로 원형 감지 + P 포지션 값 저장
    // 주민 앞 기준 백터 내적 값 설정
    // 내적 값 범위 안에 주민 포지션이 있는지 검사
    // 추가로 P 와 주민 사이에 벽이 있는지 레이 쏴서 검사.
    
    void OnTriggerEnter(Collider other)
    {
        // 주민이고, 페인트 모드라면.
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && PlayerData.Instance._isPaintMode)
        {
            _playerMarking = true;
            Debug.Log($"{gameObject.name}가 범인을 발견했습니다.");
            _neighborAgent.speed += 0.2f;
            _miniMap.color = Color.red;
            
            _Taget = other.gameObject;
        }
    }

    public IEnumerator SetTarget()
    {
            yield return new WaitForSeconds(1.5f);
            _targetDirection = _posTarget.TargetPos[Random.Range(0, _posTarget.TargetPos.Count)].transform.position;
            SetState(Walking);
    }

    public bool IsArrived() // 도착판정
    {
        return !_neighborAgent.pathPending && _neighborAgent.remainingDistance <= _neighborAgent.stoppingDistance;
    }
    
    //=========================================

    public void ChasePlayer() // 플레이어 쫒기
    {
        float distance = Vector3.Distance(transform.position,_Taget.transform.position);
        _neighborAgent.SetDestination(_Taget.transform.position); // 플레이어를 타겟으로 쫒아감.
        
        if (distance >= _detectRange) // 감지범위 밖- 놓침
        {
            _playerMarking = false;
            Debug.Log("범인 놓쳤음!");
            
            SetState(Anger);
        }
        else if (distance <= _catchRange) // 잡았음.
        {
            PlayerData.Instance._arrest = true;
            PlayerData.Instance._catchCount++;
            _playerMarking = false;
            PlayerData.Instance._playerSpeed = 0; 
            
            for (int i = _paintCanImages.Length-1; i >= 0 ; i--)
            {
                if (_paintCanImages[i].fillAmount >= 1) // 꽉찬게 있다면.
                {
                    _paintCanImages[i].fillAmount = 0;
                    break;
                }
                else if (_paintCanImages[0] && _paintCanImages[0].fillAmount <= 1)
                {
                    _paintCanImages[0].fillAmount = 0;
                    break;
                }
            }
            SetState(_anger);
        }
    }
    
    //==================================================
    public IEnumerator AfterAnger()
    {
        yield return new WaitForSeconds(3f);
        if (PlayerData.Instance._arrest)
        {
            PlayerData.Instance._arrest = false;
            _paintCan.ChackCanCount(); // 여기서 플레이어 다시 움직임 가능
        }
        
        _neighborAgent.stoppingDistance = 0.2f;
        _neighborAgent.SetDestination(PlayerData.Instance._lastBuildingMR.transform.position);
       // _neighborAgent.SetDestination(_lastBildingPos); // 마지막 건물 포지션으로 가기
        PlayerData.Instance._isDiscover =  false; // 혐의 풀림
    }

    public IEnumerator AngerToReturn()
    {
        if (!PlayerData.Instance._lastBilding._isClear)
        {
            PlayerData.Instance._lastBuildingMR.material = _resetMR;
            PlayerData.Instance._lastBilding._minimap.color = Color.darkGray;
        }
        else
        {
            Debug.Log("이미 페인트 범벅이라 돌이킬 수 없습니다... 주민은 슬퍼합니다");
        }
        
        /*if (PlayerData.Instance._lastBilding._isClear)
        {
            PlayerData.Instance._lastBilding._isClear = false;
            PlayerData.Instance._clearBildings--;
        }*/
        yield return new WaitForSeconds(2f);
        SetState(Walking);
    }
}
