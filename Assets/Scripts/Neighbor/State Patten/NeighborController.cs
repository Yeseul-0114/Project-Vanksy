using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NeighborController : MonoBehaviour, IStateEvent
{
    [SerializeField] private PosTarget _posTarget;
    [SerializeField] private float _detectRange;

  //  private float _KeepPlayerSpeed;
    public bool _arrest; // 범인 잡았니
    private Transform _playerTaget; // 범인
    internal bool _playerMarking; // 타겟 감지했음
    internal Transform _lastBildingPos; // 범행현장
    
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
      //  _KeepPlayerSpeed = PlayerData.Instance._playerSpeed;
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
            Debug.Log($"{other.gameObject.name}가 범인을 발견했습니다.");
            _lastBildingPos = other.transform;
            _playerMarking = true;
            _playerTaget = other.transform;
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
        float distance = Vector3.Distance(transform.position,_playerTaget.position);

        if (distance <= _detectRange) // 감지범위 밖- 놓침
        {
            _arrest = false;
            _neighborAgent.ResetPath();
            _playerMarking = false;
            SetState(Anger);
        }
        _neighborAgent.SetDestination(_playerTaget.position); // 플레이어를 타겟으로 쫒아감.
        
        /*if (!_neighborAgent.pathPending && _neighborAgent.remainingDistance <= _neighborAgent.stoppingDistance) //잡았음
        {
            _playerMarking = false;
            _arrest = true;
            _player.moveSpeed = 0;
            SetState(Anger);
        }*/
    }

    public IEnumerator AfterAnger()
    {
        yield return new WaitForSeconds(3f);
        if (_arrest) // 잡았다면
        {
            Debug.Log("범인 검거함!");
            // 건물 페인트 초기화
            // 페인트 통 1개 압수
        }
        else // 놓쳤다면
        {
            Debug.Log("범인 놓쳤음!");
            //건물 페인트 초기화
        }
       // PlayerData.Instance._playerSpeed = _KeepPlayerSpeed;
        _animator.SetBool("isAnger", false);
        _neighborAgent.SetDestination(_lastBildingPos.position);
       // _animator.SetBool("isDone", true);
       // StartCoroutine(AngerToReturn());
    }

    public IEnumerator AngerToReturn()
    {
        yield return new WaitForSeconds(3f);
        _targetDirection = _posTarget.TargetPos[Random.Range(0, _posTarget.TargetPos.Count)].transform.position;
        SetState(Walking);
    }
}
