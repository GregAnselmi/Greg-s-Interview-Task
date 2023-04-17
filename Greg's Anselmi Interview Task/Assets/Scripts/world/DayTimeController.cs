using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using System;

public class DayTimeController : MonoBehaviour
{
    // this value is the seconds on a day 60x60x24
    const float secondsInDay = 86400f;

    [SerializeField] Color nightLightColor;

    // this is used to change the light from day to night progressively
    [SerializeField] AnimationCurve nightTimeCurve;

    [SerializeField] Color dayLightColor = Color.white;

    float time;
    [SerializeField] float timeScale = 60f;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Light2D globalLight;
    private int days;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);    
        globalLight.color = c;
        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
