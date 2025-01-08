using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps_Animation : Moving
{
    protected override void Update()
    {
        base.Update();
        this.SetPosObj();
    }

    protected virtual void SetPosObj()
    {
        Vector2 startPos = this.PosA.transform.position;
        Vector2 endPos = this.PosB.transform.position;

        transform.position = Vector2.Lerp(startPos, endPos, Mathf.PingPong(this.moveSpeed * Time.time, 1));
    }
}
