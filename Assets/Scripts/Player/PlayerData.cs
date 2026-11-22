using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    
    // true 페인트 모드 , false는 이동모드
    public bool _isPaintMode;
    public float _playerSpeed;
    public MeshRenderer _lastBuildingMR; // 마지막으로 접근한 건물_ 범행 들켰을 때 리셋용도.
    public Building _lastBilding;
    public bool _isDiscover; // 들킨 상태인가? _ 이때는 페인트모드 할 수 없음.
    public bool _arrest; // 잡혔는가?_ 이때는 움직일 수 없음.
    public bool _canFill = true;
    
    
    public int _paintCanCount = 1;
    public bool _isStorage;
    //--------------------------------------------------

    private int _buildingCount = 18;
    private int _tree = 6;
    
    public int _catchCount = 0;
    public int _clearBildings = 0;
    public int _UseingStorage = 0;
    
    
    //--------------------------------------------------
    
    private void Awake()
    {
        SetSingleton();
        _playerSpeed = 2f;
        _isPaintMode = false;
        _isDiscover = false;
    }

    private int Ratio()
    {
        return _clearBildings / (_buildingCount + _tree) * 100;
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
       // DontDestroyOnLoad(gameObject);
    }
}
