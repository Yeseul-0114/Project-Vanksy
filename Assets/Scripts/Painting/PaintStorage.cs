using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PaintStorage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _paintButton;
    [SerializeField] private PaintCan _PaintCan;
    
    private Outline _Outline;
    private ReFillPaint _reFillPaint;
    
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
            _paintButton.text = "Useing";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerData.Instance._isStorage = false;
            _Outline.enabled = false;
            _paintButton.text = "Paint";
        }
    }

}