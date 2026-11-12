using UnityEngine;

public abstract class StateBase
{
    protected NeighborController _neiController;

    public StateBase(NeighborController neiController)
    {
        _neiController = neiController;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    
}
