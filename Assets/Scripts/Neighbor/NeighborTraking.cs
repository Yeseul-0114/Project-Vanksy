using UnityEngine;
using UnityEngine.UI;

public class NeighborTraking : MonoBehaviour
{
    // 트리거로 원형 감지 + P 포지션 값 저장
    // 주민 앞 기준 백터 내적 값 설정
    // 내적 값 범위 안에 주민 포지션이 있는지 검사
    // 추가로 P 와 주민 사이에 벽이 있는지 레이 쏴서 검사.
    
    void OnTriggerEnter(Collider other)
    {
        // 주민이고, 페인트 모드라면.
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && PlayerData.Instance._isPaintMode)
        {
            Debug.Log($"{other.gameObject.name}가 낙서범을 발견했습니다.");
        }
    } 
    
    
    
    
}
