using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] protected InputActionReference _MoveInput;
  // public float moveSpeed;
   
  private Camera _camera;
   private Animator _animator;
   private Vector2 _moveInput;
   private Vector3 _moveDirection;
   private Rigidbody _rb;
   
   private void Awake()
   {
      CaCheComponet();
   }

   private void CaCheComponet()
   {
      _rb = GetComponent<Rigidbody>();
      _animator = GetComponent<Animator>();
      _camera = Camera.main;
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
      InputMove();
   }

   private void FixedUpdate()
   {
      Move();
   }
   //----------------------------------------------------------------------

   private void InputMove()
   {
      _moveInput = _MoveInput.action.ReadValue<Vector2>();
      _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
   }

   private void Move()
   {
      if (PlayerData.Instance._isPaintMode)
      {
         _camera.transform.position += new Vector3(0, (_moveInput.y)/10, 0);
      }

      _rb.linearVelocity = _moveDirection * PlayerData.Instance._playerSpeed;
      
      if (_moveDirection != Vector3.zero)
      {
         _animator.SetInteger("Walk", 1);
         gameObject.transform.rotation = Quaternion.LookRotation(_moveDirection);
      }
      else if (_moveDirection == Vector3.zero)
      {
         _animator.SetInteger("Walk", 0);
      }
   }
   
}
