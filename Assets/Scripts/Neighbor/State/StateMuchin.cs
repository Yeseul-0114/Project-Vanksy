using UnityEngine;

public class StateMuchin : MonoBehaviour
{
   private StateBase _current;
   private IStateEvent _stateEvent;

   private void Awake()
   {
      _stateEvent = GetComponent<IStateEvent>();
   }
   private void OnEnable()
   {
      _stateEvent.OnStateChange += ChangeState;
   }
   private void OnDisable()
   {
      _stateEvent.OnStateChange -= ChangeState;
   }

   private void Update()
   {
      _current?.Update();
   }

   private void ChangeState(StateBase state)
   {
      if (state == null || _current == state) return;
      if (_current != null) return;

      _current.Exit();
      _current = state;
      _current.Enter();
   }


}
