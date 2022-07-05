using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Basic timer class. 
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    public float totalTime = 10.00f;
    public bool countDown = true;
    [SerializeField]
    private TimeDisplayType timeDisplayType = TimeDisplayType.ZeroDecimals;
    
    public static event System.Action OnCountdownTimerStart;
    public static event System.Action OnCountdownTimerStop;
    public static event System.Action OnCountdownTimerReset;
    public static event System.Action OnCountdownTimerTimesUp;

    [HideInInspector]
    public float timeLeft;
    [HideInInspector]
    public float totalPassedTime = 0;

    private bool playTimer = false;
    private enum TimeDisplayType { TwoDecimals, ZeroDecimals };
    private string displayType;
    private float timeScaleIncrease;

    private void Start()
    {
        //Time.timeScale = 1.0f;
        timeLeft = totalTime;
        timerText.text = timeLeft.ToString();

        InitTimeDisplayType();
    }

    private void InitTimeDisplayType()
    {
        switch (timeDisplayType)
        {
            // timeLeft.ToString("F") -> 2 decimal points
            // timeLeft.ToString("0") -> 0 decimal points

            case TimeDisplayType.TwoDecimals:
                displayType = "F";
                break;
            case TimeDisplayType.ZeroDecimals:
                displayType = "0";
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartTimer();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            StopTimer();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ResetTimer();
        }

        if (playTimer)
        {
            if (countDown)
            {
                UpdateTimerCountDown();
            }
            else
            {
                UpdateTimerCountUp();
            }
        }
    }

    public void SetTimer(float time)
    {
        totalTime = time;
    }

    public void StartTimer()
    {
        playTimer = true;
        OnCountdownTimerStart?.Invoke();
    }

    public void StopTimer()
    {
        playTimer = false;
        OnCountdownTimerStop?.Invoke();
    }

    public void ResetTimer()
    {
        playTimer = false;
        OnCountdownTimerReset?.Invoke();

        totalPassedTime = 0;
        timeLeft = totalTime;
        timerText.text = timeLeft.ToString();
    }

    public void AddSecondsToTimer(float seconds)
    {
        timeLeft += seconds;
    }

    public void DeductSecondsFromTimer(float seconds)
    {
        if (timeLeft - seconds > 0)
        {
            timeLeft -= seconds;
        }
        else
        {
            timeLeft = 0f;
        }
    }

    /// <summary>
    /// Increases the time scale every x seconds.
    /// Only needs to be called once and is repeated periodically after that.
    /// </summary>
    /// <param name="repeatRate">How often the time scale is increased.</param>
    /// <param name="timeScaleIncrease">The value at which the time scale is increase.</param>
    /// <param name="startTime">Start time at which the repeated function call is started.</param>
    public void IncreaseTimeScaleEveryXSeconds(float repeatRate, float timeScaleIncrease, float startTime = 0.0f)
    {
        this.timeScaleIncrease = timeScaleIncrease;
        InvokeRepeating("IncreaseTimeScaleHelper", startTime, repeatRate);
    }

    public void IncreaseTimeScale(float value)
    {
        Time.timeScale += value;
    }
    private void IncreaseTimeScaleHelper()
    {
        Time.timeScale += timeScaleIncrease;
    }

    private void UpdateTimerCountDown()
    {
        if (timeLeft <= 0)
        {
            timeLeft = 0f;
            timerText.text = "0";
            playTimer = false;
            OnCountdownTimerTimesUp?.Invoke();
            return;
        }

        timeLeft -= Time.deltaTime;

        totalPassedTime += Time.deltaTime;

        timerText.text = timeLeft.ToString(displayType);
    }

    private void UpdateTimerCountUp()
    {
        timeLeft += Time.deltaTime;

        totalPassedTime += Time.deltaTime;

        timerText.text = timeLeft.ToString(displayType);
    }
}
