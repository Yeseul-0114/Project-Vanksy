using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeiAlarm : MonoBehaviour
{
    [SerializeField] private List<GameObject>_Alarm =  new List<GameObject>(4);
    
    private Vector3 _direction;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Neighbor") &&  PlayerData.Instance._isPaintMode)
        {
            _direction = other.transform.position;
            Vector3 _playerDirection = gameObject.transform.position;
            
            if (_playerDirection.x > _direction.x && _playerDirection.y < _direction.y)
            {
               // Debug.Log($"NeiAlarm : 경고. 주민이 가까이 있습니다. 방향 : 왼쪽 + 위");
                StartCoroutine(OffAlram(0));
            }
            else if (_playerDirection.x > _direction.x && _playerDirection.y > _direction.y)
            {
               // Debug.Log($"NeiAlarm : 경고. 주민이 가까이 있습니다. 방향 : 왼쪽 + 아래");
                StartCoroutine(OffAlram(2));
            }
            else if (_playerDirection.x < _direction.x &&  _playerDirection.y < _direction.y)
            {
               // Debug.Log($"NeiAlarm : 경고. 주민이 가까이 있습니다. 방향 : 오른쪽 + 위");
                StartCoroutine(OffAlram(1));
            }
            else if (_playerDirection.x < _direction.x && _playerDirection.y > _direction.y)
            {
               // Debug.Log($"NeiAlarm : 경고. 주민이 가까이 있습니다. 방향 : 오른쪽 + 아래");
                StartCoroutine(OffAlram(3));
            }
            else return;
        }
    }

    private IEnumerator OffAlram(int index)
    {
        _Alarm[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _Alarm[index].gameObject.SetActive(false);
    }
}
