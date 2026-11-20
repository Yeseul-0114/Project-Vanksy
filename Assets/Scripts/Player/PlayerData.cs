using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    
    // true 페인트 모드 , false는 이동모드
    public bool _isPaintMode;
    public float _playerSpeed;
    public MeshRenderer _currentBuildingMR; // 마지막으로 접근한 건물.
    public bool _isDiscover; // 들킨 상태인가? _ 이때는 페인트모드 할 수 없음.
    
    
    private void Awake()
    {
        SetSingleton();
        _playerSpeed = 2f;
        _isPaintMode = false;
        _isDiscover = false;
    }

    
    
    //--------------------------------------------------
    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
