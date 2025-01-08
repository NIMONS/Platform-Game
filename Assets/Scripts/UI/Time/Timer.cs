using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : VCNVMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI timerText;
    private float startTime;
    private bool isRunning;
    public delegate void TimeStopped(float time);
    public event TimeStopped OnTimeStopped;
    private static Timer instance;
    public static Timer Instance=>instance;

    protected override void Awake()
    {
        base.Awake();
        if (Timer.instance != null) Debug.Log("Just only 1 Timer allow exists");
        Timer.instance = this;
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadTextMeshProUGUI();
    }

    protected virtual void LoadTextMeshProUGUI()
    {
        if (this.timerText != null) return;
        this.timerText=transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextMeshProUGUI", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.SetTimer();
    }

    protected override void Update()
    {
        base.Update();
        this.SettingTime();
    }

    protected virtual void SetTimer()
    {
        this.startTime = Time.time;
        this.isRunning = true;
    }


    protected virtual void SettingTime()
    {
        if (this.isRunning)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");

            timerText.text = minutes + ":" + seconds;

            if (!this.isRunning) Debug.Log("Time: " + t);
        }
    }

    public virtual void StopTimer()
    {
        if (this.isRunning)
        {
            this.isRunning=false;
            float elapsedTime=Time.time - startTime;
            OnTimeStopped?.Invoke(elapsedTime);
        }
    }


    public float GetCurrentTime()
    {
        if (!this.isRunning)
        {
            return Time.time - startTime;
        }
        else return 0f;
    }
}
