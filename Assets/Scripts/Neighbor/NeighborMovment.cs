/*
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NeighborMovment : MonoBehaviour
{
    [SerializeField] private PosTarget _posTarget;

    private NavMeshAgent _neighborAgent;
    private Vector3 _targetDirection; // 목적지 좌표. 
    private Animator _animator;
    
    private void Awake()
    {
        CacheComponent();
        RandomTarget(); // 나중에 이 부분을 집에서 나오는 걸로 지정하기. 
    }

    private void CacheComponent()
    {
        _neighborAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveTo();
        StartCoroutine(SetTarget());
    }

    private void MoveTo()
    {
       _neighborAgent.SetDestination(_targetDirection);
    }

    private bool IsArrived() // 도착판정
    {
        return !_neighborAgent.pathPending && _neighborAgent.remainingDistance <= _neighborAgent.stoppingDistance;
    }

    private IEnumerator SetTarget()
    {
        if (IsArrived())
        {
            _animator.SetBool("isGole", true);
            yield return new WaitForSeconds(1f);
            RandomTarget();
            _animator.SetBool("isGole", false);
        }
    }

    private void RandomTarget() 
    {
       _targetDirection = _posTarget.TargetPos[Random.Range(0, _posTarget.TargetPos.Count)].transform.position;
    }

}
*/
