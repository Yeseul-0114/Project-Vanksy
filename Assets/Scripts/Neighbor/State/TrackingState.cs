using UnityEngine;

public class TrackingState : StateBase
{
    public TrackingState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
        Debug.Log("TrackingState : 벽에 낙서하는 괴한을 발견했다!");
    }

    public override void Update()
    {
       
    }

    public override void Exit()
    {
      
    }
}
