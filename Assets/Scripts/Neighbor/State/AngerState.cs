using UnityEngine;

public class AngerState : StateBase
{
    public AngerState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
        Debug.Log("AngerState : 낙서범 교육하는 중...");
    }

    public override void Update()
    {
        // 애니메이션 재생
        // 전이 조건 넣어서 체크하기
        // 컨트롤러에서 셋 스테이트 실행.
    }

    public override void Exit()
    {
      
    }
}
