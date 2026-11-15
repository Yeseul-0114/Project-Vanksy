using System;
using UnityEngine;

public class NeiAlarm : MonoBehaviour
{ 
    [SerializeField] private PlayerData _playerMode;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nei") && _playerMode._isPaintMode)
        {
            Debug.Log("NeiAlarm : 경고. 주민이 가까이 왔습니다.");
        }
    }
}
