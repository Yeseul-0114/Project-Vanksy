using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
       // Debug.Log("idleState : 주민은 낙서로 골치아픕니다..");
        _neiController._animator.SetBool("isGole", true);
    }

    public override void Update()
    { 
        _neiController.StartCoroutine(_neiController.SetTarget());
        
        if (_neiController._playerMarking) // 주민 발견
        {
            _neiController.SetState(_neiController.Tracking); // 트레킹 상태로 전이
        }
    }

    public override void Exit()
    {
    }
}
