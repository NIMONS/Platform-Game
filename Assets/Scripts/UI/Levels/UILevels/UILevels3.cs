using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevels3 : UILevels
{
    protected override void Start()
    {
        base.Start();
        this.ActiveStar();
    }
    protected virtual void ActiveStar()
    {
        int completedStars = Btn_Level03.GetCompletedStars();
        for (int i = 0; i < completedStars; i++)
        {
            if (this.stars[i] == null)
            {
                Debug.Log("list Star is null");
                this.LoadStars();
                continue;
            }
            this.stars[i].gameObject.SetActive(true);
        }
    }
}
