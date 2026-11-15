using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // true 페인트 모드 , false는 이동모드
    public bool _isPaintMode;
   

    private void Awake()
    {
        _isPaintMode = false;
    }

}
