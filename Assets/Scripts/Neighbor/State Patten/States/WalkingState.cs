using UnityEditor.Tilemaps;
using UnityEngine;

public class WalkingState : StateBase
{
    public WalkingState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
       // Debug.Log("WalkingState : 순찰하는 중...");
       _neiController._animator.SetBool("haveGole", true);
    }

    public override void Update()
    {
        MoveTo();
        if (_neiController.IsArrived()) // 목표지점 도착
        {
            _neiController.SetState(_neiController.IdleState);
        }
        else if (_neiController._playerMarking) // 주민 발견
        {
            _neiController.SetState(_neiController.Tracking); // 트레킹 상태로 전이
        }
    }
    
    public override void Exit()
    {
    }

    private void MoveTo()
    {
        _neiController._neighborAgent.SetDestination(_neiController._targetDirection);
    }

}
