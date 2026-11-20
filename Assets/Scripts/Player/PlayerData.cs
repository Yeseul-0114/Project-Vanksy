using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    
    // true 페인트 모드 , false는 이동모드
    public bool _isPaintMode;
    public float _playerSpeed;
    
    private void Awake()
    {
        SetSingleton();
        _playerSpeed = 2f;
        _isPaintMode = false;
    }

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
