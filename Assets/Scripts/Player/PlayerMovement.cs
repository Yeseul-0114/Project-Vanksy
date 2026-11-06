using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] protected InputActionReference _MoveInput;
   [SerializeField] private float moveSpeed;
   private Vector2 _moveInput;
   private Rigidbody2D _rb;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody2D>();
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
      _moveInput = _MoveInput.action.ReadValue<Vector2>();
   }

   private void FixedUpdate()
   {
      _rb.velocity = _moveInput.normalized * moveSpeed;
   }
}
