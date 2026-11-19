using System;
using UnityEngine;

public class NeiAlarm : MonoBehaviour
{ 
    [SerializeField] private PlayerData _playerMode;

    private Vector3 _direction;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nei") && _playerMode._isPaintMode)
        {
            _direction = (gameObject.transform.position - other.transform.position);
            _direction.Normalize();
            Debug.Log($"NeiAlarm : 경고. 주민이 가까이 왔습니다. 방향 : {_direction}");

        }
    }
}
