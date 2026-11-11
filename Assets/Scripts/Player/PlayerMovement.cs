using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] protected InputActionReference _MoveInput;
   [SerializeField] private float moveSpeed;
   private Vector2 _moveInput;
   private Vector3 _moveDirection;
   private Rigidbody _rb;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody>();
   }

   private void OnEnable()
   {
      _MoveInput.action.Enable();
   }

   private void OnDisable()
   {
      _MoveInput.action.Disable();
   }

   private void Update()
   {
      PlayerMove();
   }

   private void FixedUpdate()
   {
      _rb.linearVelocity = _moveDirection * moveSpeed;
   }
   //----------------------------------------------------------------------

   private void PlayerMove()
   {
      _moveInput = _MoveInput.action.ReadValue<Vector2>();
      _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
   }
   
}
