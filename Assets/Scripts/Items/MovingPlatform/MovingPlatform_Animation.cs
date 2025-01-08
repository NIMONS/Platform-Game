using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_Animation : Moving
{
    [SerializeField] protected MovingPlatformCtrl movingPlatformCtrl;
    public MovingPlatformCtrl MovingPlatformCtrl => movingPlatformCtrl;
    

    protected override void Update()
    {
        base.Update();
        this.SetPositionforObj();
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadMovingPlatformCtrl();
    }

    protected virtual void LoadMovingPlatformCtrl()
    {
        if (this.movingPlatformCtrl != null) return;
        this.movingPlatformCtrl = transform.parent.parent.GetComponent<MovingPlatformCtrl>();
        Debug.LogWarning(transform.name + ": LoadMovingPlatformCtrl", gameObject);
    }

    protected virtual void SetPositionforObj()
    {
        Vector2 posA = this.PosA.transform.position;
        Vector2 posB = this.PosB.transform.position;
        Vector2 currentPos = transform.parent.position;

        float distanceToA = Vector2.Distance(currentPos, posA);
        float distanceToB = Vector2.Distance(currentPos, posB);
        if (this.isMoveToB)
        {
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, posA, this.moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.parent.position, posA) < 0.01f)
            {
                this.isMoveToB = false;
            }
        }
        else
        {
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, posB, this.moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.parent.position, posB) < 0.01f)
            {
                this.isMoveToB = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //transform.parent.SetParent(this.movingPlatformCtrl.PlayerCtrl.transform);
            this.movingPlatformCtrl.PlayerCtrl.transform.SetParent(this.transform.parent);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           //transform.parent.SetParent(null);
            this.movingPlatformCtrl.PlayerCtrl.transform.SetParent(null);
        }
    }
}
