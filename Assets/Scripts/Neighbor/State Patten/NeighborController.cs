using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NeighborController : MonoBehaviour, IStateEvent
{
    [SerializeField] private PosTarget _posTarget;

    internal NavMeshAgent _neighborAgent;
    internal Vector3 _targetDirection; // 목적지 좌표. 
    internal Animator _animator;
    internal bool _tagetMarking; // 타겟 감지했음
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
    
    //========================================================
    
    // 트리거로 원형 감지 + P 포지션 값 저장
    // 주민 앞 기준 백터 내적 값 설정
    // 내적 값 범위 안에 주민 포지션이 있는지 검사
    // 추가로 P 와 주민 사이에 벽이 있는지 레이 쏴서 검사.
    
    void OnTriggerEnter(Collider other)
    {
        // 주민이고, 페인트 모드라면.
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && PlayerData.Instance._isPaintMode)
        {
            Debug.Log($"{other.gameObject.name}가 낙서범을 발견했습니다.");
            _tagetMarking = true;
        }
    }

    public IEnumerator SetTarget()
    {
            yield return new WaitForSeconds(1f);
            _targetDirection = _posTarget.TargetPos[Random.Range(0, _posTarget.TargetPos.Count)].transform.position;
            SetState(Walking);
    }

    public bool IsArrived() // 도착판정
    {
        return !_neighborAgent.pathPending && _neighborAgent.remainingDistance <= _neighborAgent.stoppingDistance;
    }
    
}
