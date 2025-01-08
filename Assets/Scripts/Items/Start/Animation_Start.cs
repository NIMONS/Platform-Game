using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Start : VCNVMonoBehaviour
{
    [SerializeField] protected StartPoint startPoint;
    public StartPoint StartPoint => startPoint;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadStartPoint();
    }

    protected virtual void LoadStartPoint()
    {
        if (this.startPoint != null) return;
        this.startPoint=transform.parent.GetComponent<StartPoint>();
        Debug.LogWarning(transform.name + ": LoadStartPoint", gameObject);
    }

    public virtual void AnimationStart()
    {
        //Debug.Log("Đã ở chỗ bắt đầu");
        Animator animator= this.startPoint.Animator;
        if (animator == null) Debug.Log("animator is null");
        animator.SetBool("isPlayerHit", true);
    }
}
