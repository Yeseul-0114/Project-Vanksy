using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePopup : MonoBehaviour
{
    [SerializeField] private GameObject _MiniMap;
    [SerializeField] private GameObject _settings;

    public void MapPopup()
    {
        _MiniMap.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMapPopup()
    {
        _MiniMap.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void SettingPopup()
    {
        _settings.SetActive(true);
    }

    public void CloseSettingPopup()
    {
        _settings.SetActive(false);
    }

    public void LoadLobyScense()
    {
        _MiniMap.SetActive(false);
        SceneManager.LoadScene("Lobby");
    }
}
