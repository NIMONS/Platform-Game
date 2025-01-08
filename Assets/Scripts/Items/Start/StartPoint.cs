using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : VCNVMonoBehaviour
{
    [SerializeField] protected BoxCollider2D _collider;
    public BoxCollider2D Collider=> _collider;
    [SerializeField] protected Animator _animator;
    public Animator Animator=> _animator;
    [SerializeField] protected Animation_Start _Start;
    public Animation_Start Animation_Start => _Start;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadBoxCollider2D();
        this.LoadAnimator();
        this.LoadAnimation_Start();
    }

    protected virtual void LoadAnimation_Start()
    {
        if (this._Start != null) return;
        this._Start = transform.GetComponentInChildren<Animation_Start>();
        Debug.LogWarning(transform.name + ": LoadAnimation_Start", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this._animator != null) return;
        this._animator = transform.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this._collider != null) return;
        this._collider=transform.GetComponentInChildren<BoxCollider2D>();
        Debug.LogWarning(transform.name + ": LoadBoxCollider2D", gameObject);
    }
}
