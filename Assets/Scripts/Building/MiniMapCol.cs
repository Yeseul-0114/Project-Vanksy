using System;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MiniMapCol : MonoBehaviour
{
    [SerializeField] private Building _building;
    private Image _color;

    private void Awake()
    {
        _color = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _building.OnClearEvent += Oncolor;
    }

    private void OnDisable()
    {
        _building.OnClearEvent -= Oncolor;
    }

    private void Oncolor()
    {
        if (!_building._isClear) return;
        _color.color = Color.forestGreen;
    }
}
