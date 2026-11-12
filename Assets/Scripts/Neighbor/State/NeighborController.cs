using System;
using UnityEngine;

public class NeighborController : MonoBehaviour, IStateEvent
{
    private StateBase _idle;
    private StateBase _Walking;
    private StateBase _tracking;
    private StateBase _anger;
    
    public Action<StateBase> OnStateChange { get; set; }
    
    public StateBase Idle => _idle;
    public StateBase Walking => _Walking;
    public StateBase Tracking => _tracking;
    public StateBase Anger => _anger;

    private void Awake()
    {
        _idle = new IdleState(this);
        _Walking = new WalkingState(this);
        _tracking = new TrackingState(this);
        _anger = new AngerState(this);
    }

    private void Start()
    {
        OnStateChange?.Invoke(_idle);
    }
    
}
