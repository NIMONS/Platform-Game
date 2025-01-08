using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Level02 : Btn_Level
{
    [Header("Btn Level02")]
    private const string CompletedStarsKey = "CompletedStars02";
    private static Btn_Level02 _instance;
    public static Btn_Level02 Instance => _instance;
    protected override void Awake()
    {
        base.Awake();
        if (_instance != null) Debug.Log("Just only 1 Btn_Level02 allow exsist");
        _instance = this;
    }
    public static void SaveCompletedStars(int numStars)
    {
        PlayerPrefs.SetInt(CompletedStarsKey, numStars);
        PlayerPrefs.Save();
    }

    public static int GetCompletedStars()
    {
        return PlayerPrefs.GetInt(CompletedStarsKey, 0);
    }

    public virtual void HandleUILevels1(float playedTime)
    {
        int numStars = CaculaterStars(playedTime);
        Debug.Log("numStars: " + numStars);
        SaveCompletedStars(numStars);
        TimeResult.Instance.SaveTime(playedTime);
        this.SaveStars(numStars);
    }

    protected virtual int CaculaterStars(float playedTime)
    {
        if (playedTime <= 30.0f)
        {
            return 3;
        }
        else if (playedTime > 30.0f && playedTime <= 45.0f)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    protected override void LogTimePlayed(float time)
    {
        Debug.Log("Time played: " + time);
        //this.HandleUILevels1(time);
    }

    public override void PrcessedWhenEnabled()
    {
        base.PrcessedWhenEnabled();
        float playedTime = GetPlayedTime();
        //lấy được thời gian
        Debug.Log("Thời gian đã chơi là: " + playedTime);
        this.HandleUILevels1(playedTime);
    }

    protected virtual void SaveStars(int numStars)
    {
        PlayerPrefs.SetInt("SaveStars02", numStars);
        PlayerPrefs.Save();
    }
}
