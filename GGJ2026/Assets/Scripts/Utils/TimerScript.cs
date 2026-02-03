using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private float timeElapsed;

    private bool timerStarted = false;

    private void Start()
    {
        StartTimer();

        SceneManager.activeSceneChanged += SetSessionTimeSurvived;
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
            timerText.text = GetTimerText();
        }
    }

    private string GetTimerText()
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
        
        return $"{minutesText}:{secondsText}";
    }

    public void SetSessionTimeSurvived(Scene current, Scene next)
    {
        Debug.Log((SessionData.instance != null) + "at " + GetTimerText());
        
        if(SessionData.instance)
            SessionData.instance.timeSurvived = GetTimerText();
    }
}
