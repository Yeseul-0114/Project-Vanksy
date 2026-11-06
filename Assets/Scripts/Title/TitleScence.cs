using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScence : MonoBehaviour
{
    [SerializeField] private InputActionReference _toch;

    private void OnEnable()
    {
        _toch.action.Enable();
        _toch.action.performed += OnChangeScene;
    }

    private void OnDisable()
    {
        _toch.action.Disable();
        _toch.action.performed -= OnChangeScene;
    }


    public void OnChangeScene(InputAction.CallbackContext context)
    {
        Debug.Log("씬 전환_로비");
        SceneManager.LoadScene("Lobby");
    }
}
