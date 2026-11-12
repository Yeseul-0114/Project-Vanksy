using UnityEngine;

public class WalkingState : StateBase
{
    
    public WalkingState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
        Debug.Log("WalkingState : 배회하는 중...");
    }

    public override void Update()
    {
       // 애니메이션 재생
       // 전이 조건 체크
       // 컨트롤러.SetState 로 전이
    }

    public override void Exit()
    {
    }
}
