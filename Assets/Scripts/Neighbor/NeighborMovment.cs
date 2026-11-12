using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NeighborMovment : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targetPos = new List<GameObject>(11);

    private NavMeshAgent _neighborAgent;
    private Vector3 _targetDirection; // 목적지 좌표. 
    
    private void Awake()
    {
        _neighborAgent = GetComponent<NavMeshAgent>();
        RandomTarget();
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
            yield return new WaitForSeconds(0.4f);
            RandomTarget();
        }
    }

    private void RandomTarget() 
    {
       _targetDirection = _targetPos[Random.Range(0, _targetPos.Count)].transform.position;
    }

}
