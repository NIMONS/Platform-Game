using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResult
{
    public float completionTime;
    public int startsEarned;

    public LevelResult(float time)
    {
        this.completionTime= time;
        this.startsEarned = this.CaculateStarsEarned(time);
    }

    private int CaculateStarsEarned(float time)
    {
        if (time <= 30.0f)
        {
            return 3;
        }
        else if (time <= 45.0f)
        {
            return 2;
        }
        else if (time <= 60.0f)
        {
            return 1;
        }
        else
            return 0;
    }
}
