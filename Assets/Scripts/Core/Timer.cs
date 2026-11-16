using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("시간 정보")]
    [SerializeField] private float _playTime = 180;
    [SerializeField] private int _dayTime;
    [SerializeField] private int _nightTime;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _TimeText;

    [SerializeField] private Canvas _Ending;

    private bool _isDay;
    
    private void Start()
    {
        StartCoroutine(TimeOut());
        StartCoroutine(DayCycle());
    }

    private void Update()
    {
        Timer();
    }
    
    private void Timer()
    {
        _playTime -= Time.deltaTime;
        int _tempTime = (int)_playTime;
        _TimeText.text = $"남은 시간 : {_tempTime.ToString()}";
    }

    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(_playTime);
        Time.timeScale = 0;
        _Ending.gameObject.SetActive(true);
    }
    
    //-------------------------------------------------------

    private IEnumerator DayCycle()
    {
        for (int i = 0; i <= 2; i++)
        {
            _isDay = true;
            Debug.Log($"지금은 낮 입니다. {_playTime}");
            yield return new WaitForSeconds(_dayTime);
            _isDay = false;
            Debug.Log($"지금은 밤 입니다. {_playTime}");
            yield return new WaitForSeconds(_nightTime);
        }
        Debug.Log("게임 종료");
    }
}
