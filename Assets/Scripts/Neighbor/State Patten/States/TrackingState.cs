using UnityEngine;

public class TrackingState : StateBase
{
    public TrackingState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
        PlayerData.Instance._isDiscover = true;
        
        Debug.Log("범인을 추격합니다.");
        _neiController._animator.SetBool("PlayerTarget", true);
        _neiController._neighborAgent.ResetPath();
    }

    public override void Update()
    {
        _neiController.ChasePlayer();
    }

    public override void Exit()
    {
      
    }
    
    
}
