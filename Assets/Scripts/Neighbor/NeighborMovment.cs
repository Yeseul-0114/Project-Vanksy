using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NeighborMovment : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private List<Vector3> _targetPos = new List<Vector3>(11);

    private NavMeshAgent _neighborAgent;
    private Vector3 _targetDirection; // 목적지 좌표. 
    
    private void Awake()
    {
        _neighborAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        MoveTo();
    }

    private void MoveTo()
    {
       _neighborAgent.SetDestination(_targetDirection);
    }

    private void RandomTarget()
    {
        // 목적지 도착하면 실행하도록 하기
       _targetDirection = _targetPos[Random.Range(0, _targetPos.Count+1)];

    }

}
