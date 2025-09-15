using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Btn_Level : BaseButton
{
    [Header("Btn Level")]
    [SerializeField] protected string stringLoadToScene;
    [SerializeField] protected Timer time;
    protected override void Start()
    {
        base.Start();
        this.time.OnTimeStopped += LogTimePlayed;
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadTime();
    }

    protected virtual void LoadTime()
    {
        if (this.time != null) return;
        this.time = transform.parent.parent.Find("Header").GetComponentInChildren<Timer>();
        Debug.LogWarning(transform.name + ": LoadTime", gameObject);
    }
    protected override void OnClick()
    {
        //this.time.StopTimer();
        //LevelManagerr.Instance.CompleteLevel();
        SceneManager.LoadScene(stringLoadToScene);
    }

    protected virtual void LogTimePlayed(float time)
    {
        
    }

    public float GetPlayedTime()
    {
        if (this.time != null)
        {
            return time.GetCurrentTime();
        }
        else return 0f;
    }

    //được sử lý khi bật
    public virtual void PrcessedWhenEnabled()
    {
        this.time.StopTimer();
    }
}
