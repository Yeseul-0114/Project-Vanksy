using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("참조 필요")]
    [SerializeField] private TextMeshProUGUI _ClearRatio;
    
    [Header("정보 확인용")]
    public static PlayerData Instance { get; private set; }
    // true 페인트 모드 , false는 이동모드
    public bool _isPaintMode;
    public float _playerSpeed;
    public MeshRenderer _lastBuildingMR; // 마지막으로 접근한 건물_ 범행 들켰을 때 리셋용도.
    public Building _lastBilding;
    public bool _isDiscover; // 들킨 상태인가? _ 이때는 페인트모드 할 수 없음.
    public bool _arrest; // 잡혔는가?_ 이때는 움직일 수 없음.
    
    
    public float _paintCanCount = 1;
    public bool _isStorage;
    //--------------------------------------------------

    private float _buildingCount = 18;
    private float _tree = 6;
    
    public int _catchCount;
    public float _clearBildings;
    public int _UseingStorage;
    
    
    //--------------------------------------------------
    
    private void Awake()
    {
        SetSingleton();
        _playerSpeed = 2f;
        _isPaintMode = false;
        _isDiscover = false;
    }

    public void ClearRatio()
    {
        _ClearRatio.text = $" 진행도 : {(int)(_clearBildings / (_buildingCount + _tree) * 100)}%";
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
