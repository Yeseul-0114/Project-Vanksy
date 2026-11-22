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
        _neiController._animator.SetBool("isAnger", true);
        _neiController.StartCoroutine(_neiController.AfterAnger());
    }

    public override void Update()
    {
        if (_neiController.IsArrived()) // 마지막 범행현장 도착 했다면
        {
            _neiController.StartCoroutine(_neiController.AngerToReturn());
        }
    }

    public override void Exit()
    {
    }
}
