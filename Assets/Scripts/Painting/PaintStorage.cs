using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PaintStorage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _paintButton;

    private Outline _Outline;

    private void Awake()
    {
        _Outline = GetComponent<Outline>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerData.Instance._isStorage = true;
            _Outline.enabled = true;
            _paintButton.text = "Get!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerData.Instance._isStorage = false;
            _Outline.enabled = false;
           // _storagePopup[0].SetActive(false);
        }
    }

}