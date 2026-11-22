using UnityEngine;

public class AngerState : StateBase
{
    public AngerState(NeighborController neiController) : base(neiController)
    {
    }

    public override void Enter()
    {
        _neiController._neighborAgent.ResetPath();
        Debug.Log("AngerState : 주민은 분노합니다.");
        _neiController.StartCoroutine(_neiController.AfterAnger());
    }

    public override void Update()
    {
        if (_neiController.IsArrived()) // 마지막 범행현장 도착 했다면
        {
            Debug.Log("건물 청소 중");
            _neiController._neighborAgent.stoppingDistance = 0.6f;
            _neiController.StartCoroutine(_neiController.AngerToReturn());
        }
    }

    public override void Exit()
    {
    }
}
