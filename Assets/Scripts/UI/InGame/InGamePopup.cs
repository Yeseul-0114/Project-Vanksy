using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePopup : MonoBehaviour
{
    [SerializeField] private Canvas _MiniMap;
    [SerializeField] private Canvas _settings;

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
        _MiniMap.gameObject.SetActive(false);
        SceneManager.LoadScene("Lobby");
    }
}
