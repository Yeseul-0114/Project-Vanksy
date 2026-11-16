using UnityEngine;

public class MiniMapUI : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    
    private void LateUpdate()
    {
        UiRot();
    }

    private void UiRot()
    {
        transform.forward = _cam.forward;
    }
}
