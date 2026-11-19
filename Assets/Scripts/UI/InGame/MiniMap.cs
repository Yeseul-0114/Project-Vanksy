using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Image _ground;
    [SerializeField] private Image _player;
 //   [SerializeField] private Canvas _Bilding;

    private void OnEnable()
    {
        _ground.gameObject.SetActive(true);
        _player.gameObject.SetActive(true);
//        _Bilding.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _ground.gameObject.SetActive(false);
        _player.gameObject.SetActive(false);
 //       _Bilding.gameObject.SetActive(false);
    }


    // 1. 월드 내 건물들 위치값을 구한다 -> UI내에 배치
    // 2. 월드 내 플레이어 위치 값 구한다 -> UI내 배치
    // 3. 건물들 채색 완료 bool 값을 구한다 -> UI색으로 표시
    // 4. 건물들 채색 true 비율 구해서 text 띄워준다.
}
