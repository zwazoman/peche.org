using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int Hour { get; private set; }
    public int Day { get; private set; }
    public int Week { get; private set; }
    public int Month { get; private set; }

    public bool IsDay;
    public bool IsMorning;
    public bool IsNoon;
    public bool IsEvening;

    public bool IsPaused = false;

    public event Action OnHour;
    public event Action OnMorning;
    public event Action OnNoon;
    public event Action OnEvening;
    public event Action OnNight;
    public event Action OnDay;
    public event Action OnWeek;
    public event Action OnMonth;

    [SerializeField] float _hourDurationInRealSecs = 5f;

    float LastRealTickTime;
    float lastRealPauseTime;

    private static TimeManager instance = null;
    public static TimeManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void Init() => StartCoroutine(_init());

    private IEnumerator _init()
    {
        InvokeRepeating(nameof(TimePass), 0, _hourDurationInRealSecs); //repete la fonction TimePass
        yield return 0;
        OnHour?.Invoke();//premiere heure
    }

    private void Start()
    {
        Hour = 5;
        Init();
    }

    private void TimePass() //gere le temps en seconde irl pour 1h ig
    {
        LastRealTickTime = Time.time;

        Hour++;
        OnHour?.Invoke();

        switch (Hour)
        {
            case 6 :
                IsDay = true;
                OnMorning?.Invoke();
                break;
            case 12 :
                OnNoon?.Invoke();
                break;
            case 18 :
                OnEvening?.Invoke();
                break;
            case 22 :
                OnNight?.Invoke();
                break;
            case 24 :
                Day++;
                Hour = 0;
                OnDay?.Invoke();
                break;
        }

        if (Day % 7 == 0)
        {
            Week++;
            OnWeek?.Invoke();
        }
        if (Day %30 == 0)
        {
            Month++;
            OnMonth?.Invoke();
        }
    }

    public void PauseTime()
    {
        if(IsPaused) return;
        lastRealPauseTime = Time.time;
        CancelInvoke(nameof(TimePass));
        IsPaused = true;
    }

    public void Resume()
    {
        if (!IsPaused) return;

        IsPaused = false;

        float elapsedTime = lastRealPauseTime - LastRealTickTime;
        float remainingTime = _hourDurationInRealSecs - elapsedTime;

        LastRealTickTime = Time.time;
        InvokeRepeating(nameof(TimePass), remainingTime, _hourDurationInRealSecs);
    }
}
