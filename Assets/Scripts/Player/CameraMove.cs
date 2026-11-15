using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _taget;
    [SerializeField] private Vector3 _offsetPos;
    
    private void LateUpdate()
    {
        gameObject.transform.position = _taget.position + _offsetPos;
    }
}
