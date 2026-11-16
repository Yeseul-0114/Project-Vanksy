using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePopup : MonoBehaviour
{
    [SerializeField] private Canvas _MiniMap;
    [SerializeField] private Canvas _settings;

    private void Awake()
    {
        _MiniMap.gameObject.SetActive(false);
    }

    public void MapPopup()
    {
        _MiniMap.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMapPopup()
    {
        _MiniMap.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void SettingPopup()
    {
        _settings.gameObject.SetActive(true);
    }

    public void CloseSettingPopup()
    {
        _settings.gameObject.SetActive(false);
    }

    public void LoadLobyScense()
    {
        SceneManager.LoadScene("Lobby");
    }
}
