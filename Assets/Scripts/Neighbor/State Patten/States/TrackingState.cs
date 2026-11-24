using UnityEngine;

public class TrackingState : StateBase
{
    public TrackingState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
        _neiController._animator.SetBool("Target",true);
        PlayerData.Instance._isDiscover = true;
        
        Debug.Log("범인을 추격합니다.");
        _neiController._neighborAgent.speed += 1;
        _neiController._neighborAgent.ResetPath();
    }

    public override void Update()
    {
        _neiController.ChasePlayer();
    }

    public override void Exit()
    {
        _neiController._neighborAgent.speed -= 1;
    }
}
