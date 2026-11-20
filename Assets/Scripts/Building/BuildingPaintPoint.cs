using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPaintPoint : MonoBehaviour
{
    private int _pixSize;
    private int _restPixels;
    private List<int> _restPixIndex;
    
    private Material _mat;
    private Color _baseColor;
    private Texture2D _texture;
    private RenderTexture _renderTexture;

    private void Awake()
    {
        CacheTexInfo();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GetPixPoint();
        }
    }

    private void GetPixPoint()
    {
        // 픽셀 정보 읽기
        Color[] pixels = _texture.GetPixels();
        _pixSize = pixels.Length;
        _restPixels = _pixSize;

        for (int i = 0; i < pixels.Length; i++)
        {
           if (pixels[i] == _baseColor)
           {
               _restPixels--;
               _restPixIndex.Add(i); // 안칠한 픽셀 위치 인덱스 리스트에 추가(기억)
           }
        }
        
        int _paintPercent = 100 - ((_restPixels / _pixSize) * 100);
        Debug.Log(_paintPercent);
    }
    
    
    private void CacheTexInfo()
    {
        _mat = GetComponent<MeshRenderer>().material;
        _baseColor = _mat.GetColor("_Color");
        //_texture = _mat.mainTexture as Texture2D;
    }

    /*private Color[] Trans()
    {
        Graphics.Blit(_mat.mainTexture,_renderTexture);
        RenderTexture.active = _renderTexture;
        
        _texture.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
        _texture.Apply();

        return _texture.GetPixels();
    }*/
}
