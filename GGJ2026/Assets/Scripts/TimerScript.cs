using System;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private float timeElapsed;

    private bool timerStarted = false;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }

    private void Update()
    {
        if (timerStarted)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        int secondsElapsed = (int)timeElapsed;

        int minutes = secondsElapsed / 60;

        string minutesText;
        
        if(minutes < 10)
            minutesText = "0" + minutes;
        else
            minutesText = minutes.ToString();
        
        int seconds = secondsElapsed % 60;
        string secondsText;
        
        if(seconds < 10)
            secondsText = "0" + seconds;
        else
            secondsText = seconds.ToString();
        
        timerText.text = $"{minutesText}:{secondsText}";
    }
}
